<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Password_change.aspx.vb" Inherits="Exception_handling_system.Password_change" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>進貨異常處理系統</title>

     <script src="jquery-1.3.2-vsdoc.js"></script>
    <script type="text/javascript">
        (function () {
            var div1 = $("#left");
            var div2 = $("#right");

            var h = GetMaxHeight(div1, div2);
            div1.css("height", h);
            div2.css("height", h);
        });
        function GetMaxHeight(div1, div2) {
            var h1 = $(div1).attr("offsetHeight");
            var h2 = $(div2).attr("offsetHeight");
            return Math.max(h1, h2);
        }
    </script>
 <style >
        body{background: url(Image/point.bmp) left top  repeat #ffffff ;}
      html {
          min-width :1050px;
          }
</style>
</head>   
<body>
    <form id="form1" runat="server" style="font-family:Meiryo ">      
             
        
        
        <asp:Panel ID="Panel3" runat="server" Height="102px">
 <div id="left" style=" float: left; width: 431px; overflow: hidden; height: 90px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Denso_Logo_Tagline_Red_HiRes.png" Width="200px" />
     <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="25px" ForeColor="Black"  Text="進貨異常處理系統" Width="216px"  Height="27px"></asp:Label>
            
        </div>
        <div id="right" style=" margin-right :0px; float :right; ">
             <br />
             <br />
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/1.bmp"  OnClick ="ImageButton1_Click"  Height="45px" Width="130px"/>           
<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/2.bmp" OnClick="ImageButton2_Click" Height="45px" Width="130px" />             
<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/3.bmp" OnClick="ImageButton3_Click" Height="45px" Width="130px" />
<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/4.bmp" OnClick="ImageButton4_Click"  Height="45px" Width="130px"/>   
            
        </div>                       
        </asp:Panel>   
      

        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Right" Width="100%" >
   <asp:Table ID="Table4" runat="server" HorizontalAlign="Right">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><asp:Label ID="user_name" runat="server" Text="" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:Label ID="Label3" runat="server" Text="您好:" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="X-Large" >登出</asp:LinkButton>
</asp:TableCell>
                </asp:TableRow>              
            </asp:Table>
        </asp:Panel>
         
        <asp:Table ID="Table5" runat="server" HorizontalAlign="Center" Width="100%" Height="23px">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server"><asp:Label ID="Label7" runat="server" Text="密碼修改" Font-Size="30px"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>

            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                
                

            <asp:Table ID="Table1" runat="server" Height="187px" HorizontalAlign="Center" Width="35%">


                <asp:TableRow runat="server"  Width="600px">
                    <asp:TableCell runat="server" HorizontalAlign="Right">
                        <asp:Label ID="Label4" runat="server" Text="舊密碼 :"></asp:Label>
</asp:TableCell>

                    <asp:TableCell runat="server" HorizontalAlign="Left">
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
</asp:TableCell>
                                    

                </asp:TableRow>

                <asp:TableRow runat="server" Width="600px">
                    <asp:TableCell runat="server" HorizontalAlign="Right">
                        <asp:Label ID="Label6" runat="server" Text="新密碼 :"></asp:Label>
</asp:TableCell>

                    <asp:TableCell runat="server" HorizontalAlign="Left">
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox>
</asp:TableCell>
                    
                </asp:TableRow>
                <asp:TableRow runat="server" Width="600px">
                    <asp:TableCell runat="server" HorizontalAlign="Right">
                        <asp:Label ID="Label2" runat="server" Text="確認密碼 :"></asp:Label>
</asp:TableCell>

                    <asp:TableCell runat="server" HorizontalAlign="Left">
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
</asp:TableCell>
                    
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server" ColumnSpan="2">
                        <asp:Label ID="Label5" runat="server" Text="" Visible="False" ForeColor="Red"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server" ColumnSpan="2">
                        <asp:Button ID="Button1" runat="server" Text="送出"  OnClick="Button1_Click"/>
                    

</asp:TableCell>
                </asp:TableRow>


            </asp:Table>



        </asp:Panel>



    </form>
</body>
</html>
