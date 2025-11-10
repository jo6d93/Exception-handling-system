<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Exception_handling_system._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>進貨異常處理系統</title>
    <style >
        body{background: url(Image/point.bmp) left top  repeat #ffffff ;}
        h1 {color :#ff0000; font-size :300%; }        
    
.usre_name{ width:240px; height:38px; line-height:38px; border:1px solid #dfe1e8; background:url(Image/login_img_03.png)  no-repeat left center; padding-left:30px; }
.usre_name input{ width:230px; height:36px; border:1px solid #fff;color:#666;}
.usre_pass{ width:240px; height:38px; line-height:38px; border:1px solid #dfe1e8; background:url(Image/login_img_09.png)  no-repeat left center; padding-left:30px; }
.usre_pass input{ width:230px; height:36px; border:1px solid #fff;color:#666;}

html {
          min-width :1050px;
          }
    </style>
</head>
<body>    
    <form id="form1" runat="server" style ="font-family:'Meiryo UI'">
                 

        <br /><br /><br /><br /><br /><br /><br /><br />       
        
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center"  Width="100%"  Height="463px">
            <br /><br />
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Denso_Logo_Tagline_Red_HiRes.png" Width="350px" />
            <br /><br />     

            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">

<asp:Label ID="Label3" runat="server" Font-Bold="true"  Text="進貨異常處理系統" Font-Size="40px" ForeColor="#333333" ></asp:Label>

            </asp:Panel>





            <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                        
                <asp:TableRow runat="server" >
                        <asp:TableCell runat="server"  HorizontalAlign="Left" BackColor="white" VerticalAlign="Middle">
<div class="usre_name">
<div style="float: left; position:absolute;">
<asp:TextBox ID="user_id" runat="server" TabIndex="1" Height="25px" Font-Size ="15px" Text="" ToolTip="請輸入帳號"></asp:TextBox>

</div></div>
                    </asp:TableCell>
                        <asp:TableCell runat="server" RowSpan="2">
                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/enter.png" Width="50px" OnClick ="ImageButton1_Click"  />

                        </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
 <asp:TableCell runat="server" HorizontalAlign="Left" BackColor="white" VerticalAlign="Middle">
<div class="usre_pass">

<div style="float: left; position:absolute; ">
<asp:TextBox ID="user_pass" runat="server" TabIndex="2" Height="25px"  Font-Size ="15px" Text="" ToolTip="請輸入密碼" TextMode="Password"></asp:TextBox>
    

    </div></div>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><asp:Label ID="msg_error" runat="server" Text="" ForeColor="black" Font-Bold="False" Font-Size="15px" Visible ="false" ></asp:Label>
</asp:TableCell>                 
                </asp:TableRow>            
            </asp:Table>            
      </asp:Panel>      
   
          </form>     
</body>
</html>
