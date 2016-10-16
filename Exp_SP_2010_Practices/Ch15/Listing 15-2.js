<script src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>

<script type="text/javascript">
Sys.Application.add_load(App_Load);

var Exception = null;
var WebPart = null;
var Page = null;
var Shapes = null;
var CurrentShape = null;

var ContentEditor = null;
var ContentEditorDisplayPanel = null;
var ContentEditorMapPanel = null;

var SelectedSupportTier = null;  
var Context = null; 
var Web = null     
var List = null;
var Query = null;
var ListItem = null;
var ListItemCollection = null;

function App_Load() 
{
   try 
   {
      WebPart = new .Control ("WebPartQ2");
      WebPart.addHandler ("diagramcomplete", Diagram_Complete);
      WebPart.addHandler ("diagramException", Diagram_Error);

      ContentEditor = document.getElementById("WebPartQ3");
      ContentEditorDisplayPanel = document.createElement("div");      
      ContentEditorDisplayPanel.setAttribute("id", "TechnicianResults");

      ContentEditorMapPanel = document.createElement("div");
      ContentEditorMapPanel.setAttribute("id", "MapResults");
      ContentEditorMapPanel.setAttribute("style", "position: relative; width: 400px; height: 400px;");
      
      ContentEditor.appendChild(ContentEditorDisplayPanel);
      ContentEditor.appendChild(document.createElement("br"));
      ContentEditor.appendChild(ContentEditorMapPanel);

      GetMap();
      FindLoc("Winston-Salem, NC", "Headquarters");
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

      WebPart.addHandler ("shapeselectionchanged ", Shape_SelectionChanged);
   }
   catch (e)
   {
      Exception = e;
   }
}

function Diagram_Error () 
{
   WebPart.DisplayCustomMessage ("Error: " + Exception);
}

function Shape_SelectionChanged (source, id) 
{
   try 
   {
      Shapes = Page.getShapes ();
      CurrentShape= Shapes.getItemById (id);
      var ShapeData = CurrentShape.getShapeData ();
      
      for (var i = 0; i < ShapeData .length; i++) 
      {
         var fieldName = ShapeData [i].label;
         
         if (fieldName == 'Step') 
         {
            SelectedSupportTier  = ShapeData [i].formattedValue;
         }
      }
            
      GetTechnician();
   } 
   catch (e) 
   {
      Exception = e;
      Diagram_Error();
   }
}
 
function GetTechnician () 
{
   Context = new SP.ClientContext.get_current();
   Web = Context.get_web();   
   List = Web.get_lists().getByTitle("Technicians");
   
   Query = new .CamlQuery();
   Query.set_viewXml('<View><Query><Where><Eq><FieldRef Name="Support_x0020_Tier"/><Value Type="Text">'+ SelectedSupportTier +'</Value></Eq></Where></Query>' + 
                       '<ViewFields><FieldRef Name="Title"/><FieldRef Name="Location"/><FieldRef Name="Calls_x0020_Handled"/><FieldRef Name="Calls_x0020_Closed"/>' + 
                       '<FieldRef Name="Calls_x0020_Transferred"/></ViewFields></View>');   

   ListItemCollection = List.getItems(Query); 
   
   Context.load(ListItemCollection);
   
   Context.executeQueryAsync(Function.createDelegate(this, this.LoadSucceeded), Function.createDelegate(this, this.LoadFailed));
}

function LoadSucceeded(sender, args)
{
   var ListItemEnumerator = ListItemCollection.getEnumerator();
   
   ContentEditorDisplayPanel.innerHTML = '';
   
   var Table = document.createElement("table");
   Table.setAttribute ('border', '0px');
   Table.setAttribute ('cellacing', '0px');
   Table.setAttribute ('cellpadding', '3px');
   
   var HeaderRow = document.createElement('tr');
   HeaderRow.setAttribute ('class', 'ms-viewheadertr ms-vhltr');
   var TechnicianCell = document.createElement('th');
   var LocationCell = document.createElement('th');
   var CallsHandledCell = document.createElement('th');
   var CallsClosedCell = document.createElement('th');
   var CallsTransferredCell = document.createElement('th');

   TechnicianCell.innerHTML = 'Technician';
   LocationCell.innerHTML = 'Location';
   CallsHandledCell.innerHTML = 'Calls Handled';
   CallsClosedCell.innerHTML = 'Calls Closed';
   CallsTransferredCell.innerHTML = 'Calls Transferred';

   HeaderRow.appendChild(TechnicianCell);
   HeaderRow.appendChild(LocationCell);
   HeaderRow.appendChild(CallsHandledCell);
   HeaderRow.appendChild(CallsClosedCell);
   HeaderRow.appendChild(CallsTransferredCell);
   
   Table.appendChild(drHeaderRow);

    while (ListItemEnumerator.moveNext()) 
    {
       ListItem = ListItemEnumerator.get_current();
       
       var drTechnicianRow = document.createElement("tr");
       TechnicianRow.setAttribute ('class', 'ms-itmhover');
       TechnicianRow.setAttribute ('onclick', 'FindLoc ("' + ListItem.get_item("Location") + '", "' + ListItem.get_item("Title") + '");');
       
       TechnicianCell = document.createElement("td");
       LocationCell = document.createElement("td");
       CallsHandledCell = document.createElement("td");
       CallsClosedCell = document.createElement("td");
       CallsTransferredCell = document.createElement("td");
       
       TechnicianCell.innerHTML = ListItem.get_item('Title');
       LocationCell.innerHTML = ListItem.get_item('Location');
       CallsHandledCell.innerHTML = ListItem.get_item('Calls_x0020_Handled');
       CallsClosedCell.innerHTML = ListItem.get_item('Calls_x0020_Closed');
       CallsTransferredCell.innerHTML = ListItem.get_item('Calls_x0020_Transferred');
    
       TechnicianRow.appendChild(TechnicianCell);
       TechnicianRow.appendChild(LocationCell);
       TechnicianRow.appendChild(CallsHandledCell);
       TechnicianRow.appendChild(CallsClosedCell);
       TechnicianRow.appendChild(CallsTransferredCell);
       
       Table.appendChild(TechnicianRow);
   }
   
   ContentEditorDisplayPanel.appendChild(Table);
}

function LoadFailed(sender, args)
{
   alert('Failed to load');
}

var BingMap = null;
var Coordinates = "";

function GetMap() {
   try {
      BingMap = new VEMap("MapResults");
      BingMap.LoadMap();
   } catch (e) {
      alert(e.message);
   }
}
 
function GetSearchResult(layer, resultsArray, places, hasMore, veErrorMessage) {
   Coordinates = places[0].LatLong;
}

function FindLoc(location, technician) {
   try {
      BingMap.Clear();
      BingMap.Find(null, location, null, null, 0, 1, true, true, false, true, GetSearchResult);
      window.setTimeout(function() { AddPushpin(location, technician); }, 1000);
   } catch (e) {
      alert(e.message);
   }
}

function AddPushpin(location, description) {
   var shape = new VEShape(VEShapeType.Pushpin, Coordinates);
   
   BingMap.ClearInfoBoxStyles();
   shape.SetTitle('<h4>' + description + '</h4>');
   shape.SetDescription(description + ' - ' + location);
   BingMap.AddShape(shape);
}
</script>
