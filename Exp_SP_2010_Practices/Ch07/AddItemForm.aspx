<%@ Page Language="C#" masterpagefile="~masterurl/default.master" title="Untitled 1" inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" meta:webpartpageexpansion="full" %>
<asp:Content id="Content1" runat="server" contentplaceholderid="PlaceHolderMain">
<script type="text/javascript">

ExecuteOrDelayUntilScriptLoaded(MainFunction, "sp.js"); 
  
var objContext = null; 
var objWeb = null     
var objList = null;
var objItem = null;
var objListItemCreationInfo = null;


function MainFunction()
{
}

function AddItem() 
{ 

	var strTitle = document.getElementById('txtTitle').value;
	var strPrice = document.getElementById('txtPrice').value;

  
	objContext = new SP.ClientContext.get_current();
	objWeb = objContext.get_web();   
	objList = objWeb.get_lists().getByTitle("Product");
	
	objListItemCreationInfo = new SP.ListItemCreationInformation();

	objItem = objList.addItem(objListItemCreationInfo);
	objItem.set_item('Title', strTitle);
	objItem.set_item('Price', strPrice);
	objItem.update();
	
	objContext.load(objItem);

    objContext.executeQueryAsync(Function.createDelegate(this, this.AddItemSuccess), Function.createDelegate(this, this.AddItemFail));   
    
    document.getElementById('txtTitle').value = '';
	document.getElementById('txtPrice').value = '';
}   

function AddItemSuccess(sender, args) 
{
	alert('Item added successfully.');
}   

function AddItemFail(sender, args) 
{   
    alert('Item is not added.');   
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
		<td><input name="btnAdd" type="button" value="Add" onclick="javascript:AddItem();"/></td>
	</tr>
</table>
		
</asp:Content>