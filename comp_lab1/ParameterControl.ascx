<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParameterControl.ascx.cs" Inherits="comp_lab1.ParameterControl" %>
<div>
    <asp:TextBox Width="200" id="NameText" type="text" runat="server"></asp:TextBox>
    <asp:TextBox width="200" id="DescriptionText" type="text" runat="server"></asp:TextBox>
    <asp:DropDownList id="TypeSelect" onchange="" runat="server">
        <asp:ListItem>String</asp:ListItem>
        <asp:ListItem>Int</asp:ListItem>
        <asp:ListItem>Bool</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox width="200" id="TextValue" type="text" runat="server" visible="false"></asp:TextBox>
    <asp:TextBox width="200" id="NumInput" type="text" runat="server" onpast="onPast(this)" onkeydown="onKeyUp(this)" visible="false"></asp:TextBox>
    <asp:CheckBox width="204" id="CheckInput" type="checkbox" runat="server" visible="false"></asp:CheckBox>
    <asp:Button width="200" type="button" id="Delete" runat="server" Text="Delete"></asp:Button>
</div>

<script type='text/javascript'>
    var old_value = "";
    function onKeyUp(field) {
        function handle() {
            if (old_value != field.value) {
                var str_value = "";
                if (field.value.length == 1 && field.value == "-")
                    str_value = field.value;
                if (field.value.length && !str_value.length) {
                    try {
                        var int_value = parseInt(field.value);
                        if (isNaN(int_value) || field.value != int_value.toString() || int_value < -2147483648 || int_value > 2147483647) {
                            throw Error("Error");
                        }
                        str_value = int_value.toString();
                    }
                    catch (e) {
                        str_value = old_value
                    }
                }
                old_value = str_value
                field.value = str_value;
            }
        }
        setTimeout(handle, 0);
    }
    function onPast(fiald) {
        old_value = fiald.value
    }
</script>