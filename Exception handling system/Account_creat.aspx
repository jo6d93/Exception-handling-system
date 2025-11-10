<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Account_creat.aspx.vb" Inherits="Exception_handling_system.Account_creat" %>

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
   <asp:Table ID="Table2" runat="server" HorizontalAlign="Right">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"><asp:Label ID="user_name" runat="server" Text="" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:Label ID="Label3" runat="server" Text="您好:" Font-Size="X-Large"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server"><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="X-Large" >登出</asp:LinkButton>
</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">              
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" Width="100%" HorizontalAlign="Right" Height="50px">
            <asp:Table ID="Table4" runat="server" HorizontalAlign="Right">
                <asp:TableRow runat="server" HorizontalAlign="Right" VerticalAlign="Middle">
                    <asp:TableCell runat="server" HorizontalAlign="Right" VerticalAlign="Middle" ColumnSpan="2" RowSpan="2">
                         <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="Large" Visible="False">新增使用者</asp:LinkButton>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Right" VerticalAlign="Middle" ColumnSpan="2" RowSpan="2">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="Large" Visible="False">使用者資料修改</asp:LinkButton>
</asp:TableCell>
                </asp:TableRow>
            </asp:Table>                   
</asp:Panel>     
       
        <asp:Table ID="Table5" runat="server" HorizontalAlign="Center" Width="100%" Height="23px">
            <asp:TableRow runat="server" HorizontalAlign="Center">
                <asp:TableCell runat="server"><asp:Label ID="Label11" runat="server" Text="新增使用者" Font-Size="30px"></asp:Label></asp:TableCell>
            </asp:TableRow>
        </asp:Table>        
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Visible="False" Height="600px">
            <asp:Table ID="Table1" runat="server" Height="205px" HorizontalAlign="Center" Width="650px">

                <asp:TableRow runat="server" HorizontalAlign="Right" VerticalAlign="Middle" Width="150px" >
                    <asp:TableCell runat="server" HorizontalAlign="Right" Width="200px" VerticalAlign="Middle"><asp:Label ID="Label4" runat="server" Text="使用者帳號 :"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left" VerticalAlign="Middle"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Right" Width="200px" VerticalAlign="Middle"><asp:Label ID="Label5" runat="server" Text="使用者名稱 :"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left" VerticalAlign="Middle"><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Left" VerticalAlign="Middle" Width="200px">
                    <asp:TableCell runat="server" HorizontalAlign="Right" Width="160px"><asp:Label ID="Label6" runat="server" Text="使用者職稱 :"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Right" Width="160px"><asp:Label ID="Label7" runat="server" Text="使用者部門 :"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server">
                     <asp:TableCell runat="server" HorizontalAlign="Right" Width="200px"><asp:Label ID="Label10" runat="server" Text="使用者E-Mail :"></asp:Label>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
</asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Right" Width="160px"><asp:Label ID="Label8" runat="server" Text="使用者權限 :"></asp:Label>
</asp:TableCell>
                       <asp:TableCell runat="server" HorizontalAlign="Left">
                           <asp:DropDownList ID="DropDownList1" runat="server" Width="130px" AutoPostBack ="true" OnSelectedIndexChanged ="DropDownList1_SelectedIndexChanged"  ></asp:DropDownList>
</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" HorizontalAlign="Right"><asp:Label ID="Label14" runat="server" Text="是否為第一接收者:"></asp:Label></asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:DropDownList ID="DropDownList3" runat="server" Width="130px"></asp:DropDownList></asp:TableCell>
                  <asp:TableCell runat="server" HorizontalAlign="Right"><asp:Label ID="Label20" runat="server" Text="是否為CC:"></asp:Label></asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:DropDownList ID="DropDownList4" runat="server" Width="130px"></asp:DropDownList></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                      <asp:TableCell runat="server" HorizontalAlign="Right"><asp:Label ID="Label19" runat="server" Text="是否接收每日通知:"></asp:Label></asp:TableCell>
                    <asp:TableCell runat="server" HorizontalAlign="Left"><asp:DropDownList ID="DropDownList7" runat="server" Width="130px"></asp:DropDownList></asp:TableCell>                  
                </asp:TableRow>

                <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server" ColumnSpan="4"><asp:Label ID="Label9" runat="server" Visible="False"></asp:Label>
</asp:TableCell>
                </asp:TableRow>             
            </asp:Table>
            
            <asp:Panel ID="Panel6" runat="server" HorizontalAlign="Left" Height="399px">


                <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="50%">

                    <asp:TableRow runat="server" Width="200px" HorizontalAlign="Center">
                        <asp:TableCell runat="server" Width="200px" HorizontalAlign="Center"> <asp:Label ID="Label13" runat="server" Text="第一接收者:"  Visible="true"></asp:Label></asp:TableCell>                     
                        <asp:TableCell runat="server" Width="200px" HorizontalAlign="Center">  <asp:Label ID="Label16" runat="server" Text="發送對象:" Visible="true"></asp:Label></asp:TableCell>
                        <asp:TableCell runat="server" Width="200px" HorizontalAlign="Center"> <asp:Label ID="Label15" runat="server" Text="CC:"  Visible="true"></asp:Label></asp:TableCell>                                    
                    </asp:TableRow>
                    <asp:TableRow runat="server" HorizontalAlign="Center">
                        <asp:TableCell runat="server" HorizontalAlign="Center"><asp:DropDownList ID="DropDownList5" runat="server" Width="120px" Visible="true" ></asp:DropDownList>
</asp:TableCell>                  
                        <asp:TableCell runat="server" HorizontalAlign="Center"><asp:DropDownList ID="DropDownList2" runat="server" Width="120px" Visible="true"></asp:DropDownList>
</asp:TableCell>
                        <asp:TableCell runat="server" HorizontalAlign="Center"><asp:DropDownList ID="DropDownList6" runat="server" Width="120px"  Visible="true"></asp:DropDownList>
</asp:TableCell>                       
                    </asp:TableRow>
                </asp:Table>                
            <br />      


                <asp:Panel ID="Panel8" runat="server" HorizontalAlign="Center">
        <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
  <asp:TableRow runat="server" HorizontalAlign="Center">
                    <asp:TableCell runat="server" ColumnSpan="2">
                        <asp:Button ID="Button1" runat="server" Text="送出" OnClick ="Button1_Click" Visible="False" />                        
                        <asp:Button ID="Button3" runat="server" Text="修改" OnClick ="Button3_Click" Visible="False" />
</asp:TableCell>
                     <asp:TableCell runat="server" ColumnSpan="2">
                        <asp:Button ID="Button4" runat="server" Text="查詢"  OnClick ="Button4_Click" Visible="False" />                        
                        </asp:TableCell>
                </asp:TableRow>

        </asp:Table>
</asp:Panel>
                


                <asp:Panel ID="Panel7" runat="server" Height="301px" HorizontalAlign="Center">
         <asp:Label ID="Label18" runat="server" Text="權限選擇參考如下:" Font-Size="Larger"></asp:Label>

               <asp:GridView ID="GridView1" runat="server"  CellPadding="4" ForeColor="Black" GridLines="Horizontal" 
            HorizontalAlign="Center" AutoGenerateColumns="False"  BackColor="White" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" Width="150px" >
                   <Columns>
                        <asp:TemplateField HeaderText="登入者" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="登入者" runat ="server" Text='<%#Eval("填寫者")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="發送對象" ItemStyle-Width="60px">
                    <ItemTemplate >
                        <asp:Label ID ="發送對象" runat ="server" Text='<%#Eval("檢討者")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width ="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
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
                 
                  
            <br />      
        </asp:Panel>
        </asp:Panel>
        </asp:Panel>


        <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" Visible="False">

            <asp:Label ID="Label2" runat="server" Text="使用者帳號:"></asp:Label>

            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            

            <asp:Button ID="Button2" runat="server" Text="查詢" OnClick ="Button2_Click"/>
            <br/>
            <asp:Label ID="Label12" runat="server" Text="" Visible="False"></asp:Label>
        </asp:Panel>



      
         
         
      
    </form>
</body>
</html>
