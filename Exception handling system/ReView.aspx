<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReView.aspx.vb" Inherits="Exception_handling_system.ReView1" %>

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
   <asp:Table ID="Table1" runat="server" HorizontalAlign="Right">
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
                 
        <asp:Table ID="Table4" runat="server" HorizontalAlign="Center" Width="100%" Height="16px">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server"><asp:Label ID="Label4" runat="server" Text="簽核處理" Font-Size="30px"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100%">
                       
            <asp:DropDownList ID="DropDownList3" runat="server" Width="60px">
            </asp:DropDownList>
            <asp:Label ID="Label7" runat="server" Text="年"></asp:Label>
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="60px">
            </asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Text="月"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" Width="60px">
            </asp:DropDownList>
            <asp:Label ID="Label6" runat="server" Text="日"></asp:Label>
            <asp:Button ID="btn_select" runat="server" Text="查詢" />
            <br />
            <asp:Label ID="Label13" runat="server" Text="尚無須簽核的資料" Visible="False"></asp:Label>
</asp:Panel>

            <asp:Panel ID="Panel4" runat="server">
          <asp:GridView ID="GridView1" runat="server" Width="90%" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  AllowPaging ="True" OnPageIndexChanging="GridView1_PageIndexChanging"
            HorizontalAlign="Center" AutoGenerateColumns="False"  OnRowCommand="GridView1_RowCommand"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#333333">
                         
                           <Columns>
                <asp:TemplateField HeaderText="項次" >
                    <ItemTemplate >
                        <asp:Label ID ="no" runat ="server" Text='<%#Eval("no")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異常種類" >
                    <ItemTemplate >
                        <asp:Label ID ="異常種類" runat ="server" Text='<%#Eval("異常種類")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="異常對象" >
                    <ItemTemplate >
                        <asp:Label ID ="異常對象" runat ="server" Text='<%#Eval("異常對象")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="200px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="計畫時間" >
                    <ItemTemplate >
                        <asp:Label ID ="計畫時間" runat ="server" Text='<%#Eval("計畫時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="實際時間" >
                    <ItemTemplate >
                        <asp:Label ID ="實際時間" runat ="server" Text='<%#Eval("實際時間")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="異常調查原因" >
                    <ItemTemplate >
                        <asp:Label ID ="異常調查原因" runat ="server" Text='<%#Eval("異常調查原因")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="再發防止" >
                    <ItemTemplate >
                        <asp:Label ID ="再發防止" runat ="server" Text='<%#Eval("再發防止")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="責任部屬" >
                    <ItemTemplate >
                        <asp:Label ID ="責任部屬" runat ="server" Text='<%#Eval("責任部屬")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="簽核狀態" >
                    <ItemTemplate >
                        <asp:Label ID ="簽核狀態" runat ="server" Text='<%#Eval("簽核狀態")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField> 
                         <asp:TemplateField HeaderText="" >
                    <ItemTemplate >
                        <asp:Button ID="detail" runat="server"  CommandArgument='<%#Eval("異常編號")%>' Text  ="詳情" CommandName="detail" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="" >
                    <ItemTemplate >
                        <asp:Button ID="check" runat="server"  CommandArgument='<%#Eval("異常編號")%>' Text  ="簽核" CommandName="check" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
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
         <br/>          
        </asp:Panel>      
       <asp:Panel ID="Panel5" runat="server" HorizontalAlign="Center">
                        <asp:Label ID="Label10" runat="server" Text="請先選擇要查詢的資料" Font-Size="X-Large" ForeColor="Red" Visible ="false" ></asp:Label>
        </asp:Panel>

        


        <asp:Panel ID="Panel6" runat="server">
                  
            <asp:Table ID="Table2" runat="server" Height="200px"  BorderColor="#CCCCCC" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="2px" Width="100%"  CellSpacing ="0">
                <asp:TableRow runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" Width="50" VerticalAlign="Middle" HorizontalAlign="Center">
                        <asp:Button ID="Button4" runat="server" Text="駁回" OnClick ="Button4_Click"   />
                        <asp:Button ID="Button11" runat="server" Text="取消駁回"   OnClick ="Button11_Click"  Visible ="false" />                        
<asp:Label ID ="Label2" runat="server" Text="已通知重寫"  Width="100px" Visible ="false" ></asp:Label>

</asp:TableCell>                   
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" Width="110" HorizontalAlign="Center" VerticalAlign="Middle">
                        <asp:Label ID="Label8" runat="server" Text="異常調查原因" Font-Size="Large" Width="110px"></asp:Label>                 
                        
</asp:TableCell>
                     <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle">
                         <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="120px" Width="300px" Font-Size="Large" ></asp:TextBox>                    
                         

</asp:TableCell>                   
                 
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle" Width="300px">
                        <asp:GridView ID="GridView4" runat="server" Width="300px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  AllowPaging ="True"
            HorizontalAlign="Center" AutoGenerateColumns="False"    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"  PageSize ="2" OnPageIndexChanging="GridView4_PageIndexChanging" >
                             <Columns>                          

<asp:TemplateField HeaderText="修改者" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="修改者" runat ="server" Text='<%#Eval("修改者")%>'></asp:Label>
</ItemTemplate> 


<ItemStyle Width ="80px" HorizontalAlign="Center"/>  

</asp:TemplateField>                          

<asp:TemplateField HeaderText="修改時間" ItemStyle-Width="200px">
                    <ItemTemplate >
                        <asp:Label ID ="修改時間" runat ="server" Text='<%#Eval("修改時間")%>'></asp:Label>                    
</ItemTemplate>
<ItemStyle Width ="200px" HorizontalAlign="Center"/>

</asp:TemplateField>                     

</Columns>
</asp:GridView>
</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" Width="100" VerticalAlign="Middle">
                        <asp:Button ID="Button5" runat="server" Text="駁回" OnClick ="Button5_Click"     />
                        <asp:Button ID="Button10" runat="server" Text="取消駁回" OnClick ="Button10_Click"   Visible ="false"  />
                        <asp:Label ID="Label14" runat="server" Text="已通知重寫"  Width="100px" Visible ="false" ></asp:Label>

</asp:TableCell>
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" Width="110" HorizontalAlign="Center" VerticalAlign="Middle">
                        <asp:Label ID="Label15" runat="server" Text="再發防止" Font-Size="Larger"></asp:Label>                   

</asp:TableCell>
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Center" VerticalAlign="Middle">
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Height="120px" Width="300px" Font-Size="Large"></asp:TextBox>                

</asp:TableCell>
                                       
                   
                    <asp:TableCell runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2" Width="300px">
                        <asp:GridView ID="GridView3" runat="server" Width="300px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  AllowPaging ="True"
            HorizontalAlign="Center" AutoGenerateColumns="False"    BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"  PageSize ="2" OnPageIndexChanging="GridView3_PageIndexChanging"  >
                             <Columns>
<asp:TemplateField HeaderText="修改者" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="修改者" runat ="server" Text='<%#Eval("修改者")%>'></asp:Label>                    
</ItemTemplate>
<ItemStyle Width ="80px" HorizontalAlign="Center"/>

</asp:TemplateField> 

<asp:TemplateField HeaderText="修改時間" ItemStyle-Width="100px">
                    <ItemTemplate >
                        <asp:Label ID ="修改時間" runat ="server" Text='<%#Eval("修改時間")%>'></asp:Label>
</ItemTemplate>  
    
<ItemStyle Width ="200px" HorizontalAlign="Center"/>
</asp:TemplateField>                     

</Columns>                        

</asp:GridView>
</asp:TableCell>
                </asp:TableRow>
            </asp:Table>      
              </asp:Panel>
        <asp:Panel ID="Panel7" runat="server" HorizontalAlign="Center" Width="100%" Visible ="false"  >
               
           
            <asp:Table ID="Table6" runat="server" Height="100px" HorizontalAlign="Left" Width="350px"  >
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" ColumnSpan="3"> <asp:Label ID="Label17" runat="server" Text="異常調查原因駁回原因:"></asp:Label>
</asp:TableCell>

                    <asp:TableCell runat="server" ColumnSpan="3"> <asp:TextBox ID="TextBox4" runat="server" Height="100px" TextMode="MultiLine" Width="200px"></asp:TextBox>
</asp:TableCell>

                    <asp:TableCell runat="server" ColumnSpan="3"> <asp:Button ID="Button6" runat="server" Text="送出"   OnClick ="Button6_Click"  />
</asp:TableCell>

                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <asp:Panel ID="Panel8" runat="server" HorizontalAlign="Center" Width="100%"   Visible ="false" >
            <asp:Table ID="Table7" runat="server" Height="100px" HorizontalAlign="Left" Width="350px">
                <asp:TableRow runat="server">
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" ColumnSpan="3"><asp:Label ID="Label18" runat="server" Text="再發防止駁回原因:"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" ColumnSpan="3"> <asp:TextBox ID="TextBox6" runat="server" Height="100px" TextMode="MultiLine" Width="200px"></asp:TextBox>
</asp:TableCell>
                    <asp:TableCell runat="server" ColumnSpan="3"> <asp:Button ID="Button7" runat="server" Text="送出"   OnClick ="Button7_Click"  />
</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        <asp:Panel ID="Panel9" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label19" runat="server" Text="" Font-Size="30px" Visible ="true" ></asp:Label>
            <br />
            <br />
            <br />
            <asp:Panel ID="Panel10" runat="server" Visible="false" >
                <asp:Label ID="Label20" runat="server" Text="輸入沒有00和01的異常編號:"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                <asp:Button ID="Button12" runat="server" Text="Button" OnClick ="Button12_Click" />
            </asp:Panel>
</asp:Panel>
    </form>
</body>
</html>
