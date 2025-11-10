<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Home_page.aspx.vb" Inherits="Exception_handling_system.Home_page" %>

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
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Image/1.bmp"  OnClick ="ImageButton1_Click"  Visible ="false"  Height="45px" Width="130px"/>           
<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Image/2.bmp" OnClick="ImageButton2_Click"  Visible ="false"  Height="45px" Width="130px" />             
<asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Image/3.bmp" OnClick="ImageButton3_Click"  Visible ="false" Height="45px" Width="130px" />
<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/4.bmp" OnClick="ImageButton4_Click"   Visible ="false" Height="45px" Width="130px"/>   
            
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
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" ColumnSpan="1">
<asp:LinkButton ID="LinkButton7" runat="server" Font-Size="Large" Visible="False">新增使用者</asp:LinkButton>

                    </asp:TableCell>
                    <asp:TableCell runat="server" ColumnSpan="1">
<asp:LinkButton ID="LinkButton6" runat="server" Font-Size="Large" Visible="False">修改延遲時間</asp:LinkButton>

                    </asp:TableCell>
                      <asp:TableCell runat="server" ColumnSpan="1">
<asp:LinkButton ID="LinkButton8" runat="server" Font-Size="Large" Visible="true">修改密碼</asp:LinkButton>

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

                   
         
        
        <asp:Table ID="Table5" runat="server" HorizontalAlign="Center" Width="100%" Height="23px">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server"><asp:Label ID="Label5" runat="server" Text="到貨進度一覽" Font-Size="30px"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>    
       
            
        <asp:Panel ID="Panel1" runat="server" Height="266px" HorizontalAlign="Center">            
              
            <asp:Table ID="Table1" runat="server" Height="22px" HorizontalAlign="Center" Width="350px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><asp:Label ID="Label2" runat="server" Text="受入場選擇:" Font-Size="X-Large"></asp:Label></asp:TableCell>
                    <asp:TableCell runat="server"><asp:DropDownList ID="DropDownList1" runat="server" Width="200" AutoPostBack="True"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
              
            <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Right" Width="90%">
 <asp:Label ID="Label4" runat="server" Text="無異常" BackColor="White" BorderColor="White" BorderStyle="Solid"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="進貨時間異常" BackColor="#FFC080" BorderColor="#FFC080" BorderStyle="Solid"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="作業時間異常" BackColor="LightCoral" BorderColor="LightCoral" BorderStyle="Solid"></asp:Label>
            
            </asp:Panel>        
         
        <asp:GridView ID="GridView1" runat="server" Width="90%" CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
            HorizontalAlign="Center" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowCommand ="GridView1_RowCommand" BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" >

            <Columns>
                <asp:TemplateField HeaderText="No" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("no")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="到貨日期" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="到貨日期" runat ="server" Text='<%#Eval("日期")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="預定時間" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="預定時間" runat ="server" Text='<%#Eval("預定時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="到貨時間" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="到貨時間" runat ="server" Text='<%#Eval("到貨時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="供應商" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="供應商" runat ="server" Text='<%#Eval("供應商")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="200px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="點收數" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="點收數" runat ="server" Text='<%#Eval("點收數")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="作業預定時間" ItemStyle-Width="150px">
                    <ItemTemplate >
                        <asp:Label ID ="作業預定時間" runat ="server" Text='<%#Eval("作業預定時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="150px" HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:TemplateField> 
                 <asp:TemplateField HeaderText="作業完成時間" ItemStyle-Width="150px">
                    <ItemTemplate >
                        <asp:Label ID ="作業完成時間" runat ="server" Text='<%#Eval("作業完成時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="150px" HorizontalAlign="center" VerticalAlign="Middle" />
                </asp:TemplateField>  
                        <asp:TemplateField HeaderText="到貨異常" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="到貨異常" runat ="server" Text='<%#Eval("到貨異常")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="時間異常" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="時間異常" runat ="server" Text='<%#Eval("時間異常")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="150px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>               
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        </asp:Panel>
    </form>
</body>
</html>
