<script type="text/javascript">
Sys.Application.add_load(App_Load);

var Exception;
var WebPart;
var Page;
var Shapes;
var CurrentShape;

function App_Load() 
{
   try 
   {
      WebPart = new Vwa.VwaControl ("{WebPartID}");
      WebPart.addHandler ("diagramcomplete", Diagram_Complete);
      WebPart.addHandler ("diagramException", Diagram_Exception);
   } 
   catch (e) 
   {
      Exception = e;
   }
}

function Diagram_Complete () 
{
   try 
   {      
      Page = WebPart.getActivePage ();
      Shapes = Page.getShapes ();
      CurrentShape = Page.getSelectedShape ();

      WebPart.addHandler ("Shapeselectionchanged", Shape_SelChanged);
      WebPart.addHandler ("shapemouseenter", Shape_MouseEnter);
      WebPart.addHandler ("shapemouseleave", Shape_MouseLeave);
   }
   catch (e)
   {
      Exception = e;
   }
}

function Diagram_Exception () 
{
   WebPart.displayCustomMessage ("Exception: " + Exception);
}

function Shape_SelChanged (id) 
{
	if (CurrentShape != null) 
       {
	   CurrentShape.removeHighlight ();
	}

   CurrentShape = Page.getSelectedShape ();
   CurrentShape.addHighlight (5, "#FF0000");
}

function Shape_MouseEnter (source, id) 
{
   try 
   {
      Shapes = Page.getShapes ();
      var shape = Shapes.getItemById (id);
      var shapeData = shape.getShapeData ();
      
      var shapeInfo = '';

// iterate through all data fields and add them to the overlay


      for (var i = 0; i < shapeData.length; i++) 
      {
         var field = shapeData[i].label;
         var value = shapeData[i].formattedValue;

         shapeInfo = shapeInfo + field + ' = ' + value + '<LineBreak/>';
      }
     
      
      shapeInfo = '<ContentControl Width="300"><TextBlock TextWrapping="Wrap">'+shapeInfo+'</TextBlock></ContentControl>';

// Add the overlay to the shape, using the div element as content
      shape.addOverlay (
         'dataoverlay',                 // id of the overlay 
         shapeInfo,                     // XAML content to display
         Vwa.HorizontalAlignment.right, // horizontal alignment
         Vwa.VerticalAlignment.middle,  // vertical alignment
         300,                           // 300 pixels wide 
         300);                          // 300 pixels tall

   }
   catch (e)
   {
      Exception = e;
   }

}

function Shape_MouseLeave (source, id) 
{
   try {
      var shape = Shapes.getItemById (id);
      shape.removeOverlay('dataoverlay');   
   }
   catch (e)
   {
      Exception = e;
   }
}
</script>
