<%@ Page Language="C#" masterpagefile="~masterurl/default.master" title="Untitled 1" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" meta:webpartpageexpansion="full" %>
<asp:Content id="Content1" runat="server" contentplaceholderid="PlaceHolderMain">
<script type="text/javascript">

ExecuteOrDelayUntilScriptLoaded(MainFunction, "sp.js"); 

var strID = null;  
var objContext = null; 
var objWeb = null     
var objList = null;
var objItem = null;
var objCollectionListItem = null;

function MainFunction()
{
	strID = QueryString("ID");
	
	objContext = new SP.ClientContext.get_current();
	objWeb = objContext.get_web();   
	objList = objWeb.get_lists().getByTitle("Product");
	
	var objQuery = new SP.CamlQuery();
	objQuery.set_viewXml('<View><Query><Where><Eq><FieldRef Name="ID"/><Value Type="Number">'+ strID +'</Value></Eq></Where></Query><ViewFields><FieldRef Name="Title"/><FieldRef Name="Price"/></ViewFields></View>');   
	objCollectionListItem = objList.getItems(objQuery); 
	
	objContext.load(objCollectionListItem);
	
	objContext.executeQueryAsync(Function.createDelegate(this, this.LoadItemSuccess), Function.createDelegate(this, this.LoadItemFail)); 
	
}

function LoadItemSuccess(sender, args)
{
	var listItemEnumerator = objCollectionListItem.getEnumerator();
    
    //This loop will run only once
    while (listItemEnumerator.moveNext()) 
    {
    	var objTempItem = listItemEnumerator.get_current();

    	document.getElementById('txtTitle').value = objTempItem.get_item('Title');
		document.getElementById('txtPrice').value = objTempItem.get_item('Price');
	}
}

function LoadItemFail(sender, args)
{
	alert('Item loading failed.');
}

function QueryString(parameter) 
{
	var loc = location.search.substring(1, location.search.length);
	var param_value = false;
	var params = loc.split("&");
	
	for (i=0; i<params.length;i++) 
	{
		param_name = params[i].substring(0,params[i].indexOf('='));
		if (param_name == parameter) 
		{
			param_value = params[i].substring(params[i].indexOf('=')+1)
		}
	}

	if (param_value) 
	{
		return param_value;
	}
	else 
	{
		return false;
	}
}


function UpdateItem() 
{ 

	var strTitle = document.getElementById('txtTitle').value;
	var strPrice = document.getElementById('txtPrice').value;

  
	objContext = new SP.ClientContext.get_current();
	objWeb = objContext.get_web();   
	objList = objWeb.get_lists().getByTitle("Product");

	objItem = objList.getItemById(strID);
	objItem.set_item('Title', strTitle);
	objItem.set_item('Price', strPrice);
	objItem.update();

    objContext.executeQueryAsync(Function.createDelegate(this, this.UpdateItemSuccess), Function.createDelegate(this, this.UpdateItemFail));   
    
    document.getElementById('txtTitle').value = '';
	document.getElementById('txtPrice').value = '';
}   

function UpdateItemSuccess(sender, args) 
{
	alert('Item updated successfully.');
}   

function UpdateItemFail(sender, args) 
{   
    alert('Item is not updated.');   
}

function DeleteItem() 
{ 
	if(window.confirm('Are you sure you want to delete this item?'))
	{
		objContext = new SP.ClientContext.get_current();
		objWeb = objContext.get_web();   
		objList = objWeb.get_lists().getByTitle("Product");
	
		objItem = objList.getItemById(strID);
		objItem.deleteObject();
	
	    objContext.executeQueryAsync(Function.createDelegate(this, this.DeleteItemSuccess), Function.createDelegate(this, this.DeleteItemFail));   
	}    
}

function DeleteItemSuccess(sender, args) 
{
	window.location = "AllItems.aspx";
}   

function DeleteItemFail(sender, args) 
{   
    alert('Item is not updated.');   
}

   
</script>

<table style="width: 100%">
	<tr>
		<td>Title</td>
		<td><input  id="txtTitle" name="txtTitle" type="text" /></td>
</tr>
	<tr>
		<td>Price</td>
		<td><input id="txtPrice" name="txtPrice" type="text" /></td>
	</tr>
	<tr>
		<td></td>
		<td><input name="btnUpdate" type="button" value="Update" onclick="javascript:UpdateItem();"/>
		<input name="btnDelete" type="button" value="Delete" onclick="javascript:DeleteItem();"/></td>
	</tr>
</table>
		
</asp:Content>