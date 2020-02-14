Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports Oracle.ManagedDataAccess.Client

Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        TestOracleConnection()
    End Sub
    Public Sub TestOracleConnection()
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Try
            rvConn.Open()
            MessageBox.Show("Oracle Connection OK")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show("No Oracle Connection established")
        Finally
            rvConn.Close()
        End Try
    End Sub

    Public Function CreateConnection() As Oracle.ManagedDataAccess.Client.OracleConnection
        Dim rvConn As New Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn.ConnectionString = GetConnectionString()
        Return rvConn
    End Function

    Public Function GetConnectionString() As String
        Dim vConnStr As String
        vConnStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)"
        vConnStr = vConnStr & "(HOST=feenix-oracle.swin.edu.au)(PORT=1521))"
        vConnStr = vConnStr & "(CONNECT_DATA=(SERVICE_NAME=dms)));"
        vConnStr = vConnStr & "User Id=s102254841;"
        vConnStr = vConnStr & "Password=9930942566;"
        Return vConnStr
    End Function


    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ''add customer
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "ADD_CUST_TO_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox1.Text)
            rvCmd.Parameters.Add(New OracleParameter("pCustName", OracleDbType.NChar)).Value = TextBox2.Text

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Customer Added OK"

            'MsgBox("Total number of Tables is " & vStr)
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Accept ID
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        'Handles customer name
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '' Delete All Customers from DB
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()

            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_ALL_CUSTOMERS_FROM_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "vCount"
            poracle.DbType = DbType.Int32
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("vCount").Value.ToString
            RichTextBox1.Text = "All Customer Deleted"
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try


    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        ''Product Cost TextBox
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        ''Product Name TextBox
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'Product ID Textbox
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        ''Product ID Label
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        ''Product Name Lable
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        ''Product Cost Lable
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '' Add Product Button
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "ADD_PRODUCT_TO_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pprodid", OracleDbType.Int64)).Value = Integer.Parse(TextBox3.Text)
            rvCmd.Parameters.Add(New OracleParameter("pprodname", OracleDbType.NChar)).Value = TextBox4
            rvCmd.Parameters.Add(New OracleParameter("pprice", OracleDbType.Int64)).Value = Integer.Parse(TextBox5.Text)


            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Product Added OK"

            'MsgBox("Total number of Tables is " & vStr)
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '' Delete All Products Button
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_ALL_PRODUCTS_FROM_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "rCount"
            poracle.DbType = DbType.Int32
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("rCount").Value.ToString
            RichTextBox1.Text = "All Products Deleted"
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        ''Product ID for fetching record


    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ''Customer ID for get_all_product_Details_From_DB
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter
        ''Dim custidvalue As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "GET_CUST_STRING_FROM_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "statement_line"
            poracle.DbType = DbType.String
            poracle.Size = 200
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            ''rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox6.Text)
            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "pcustid"
            poracle.DbType = DbType.Int32
            poracle.Value = Integer.Parse(TextBox6.Text)
            ''poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvConn.Open()

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("statement_line").Value.ToString
            RichTextBox1.Text = "Customer details :"
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        ''get customer details lable
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        ''cust ID for updating salesYTD
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        ''Cost for updating salesYTD
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            ''Button for updating salesYTD
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "UPD_CUST_SALESYTD_IN_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox7.Text)
            rvCmd.Parameters.Add(New OracleParameter("pamt", OracleDbType.Int64)).Value = TextBox8.Text
            rvConn.Open()

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Update OK"
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ''Prod ID to get all the product details
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter
        ''Dim custidvalue As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "GET_PROD_STRING_FROM_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "statement_one"
            poracle.DbType = DbType.String
            poracle.Size = 200
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "pprodid"
            poracle.DbType = DbType.Int32
            poracle.Value = Integer.Parse(TextBox9.Text)
            rvCmd.Parameters.Add(poracle)

            rvConn.Open()

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("statement_one").Value.ToString
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub







    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            ''Button for updating salesYTD
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "UPD_PROD_SALESYTD_IN_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            rvCmd.Parameters.Add(New OracleParameter("pProdId", OracleDbType.Int64)).Value = Integer.Parse(TextBox11.Text)
            rvCmd.Parameters.Add(New OracleParameter("pamt", OracleDbType.Int64)).Value = TextBox16.Text
            rvConn.Open()

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Update OK"
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try

    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            ''Button for updating status of customer
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "UPD_CUST_STATUS_IN_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            rvCmd.Parameters.Add(New OracleParameter("pcustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox13.Text)
            rvCmd.Parameters.Add(New OracleParameter("pstatus", OracleDbType.NChar)).Value = TextBox12.Text
            rvConn.Open()

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Update OK"
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        '' cust status update
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        '' Cust Status update
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "ADD_SIMPLE_SALE_TO_DB;"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pcustid", OracleDbType.Int64)).Value = Integer.Parse(TextBox14.Text)
            rvCmd.Parameters.Add(New OracleParameter("pprodid", OracleDbType.Int64)).Value = Integer.Parse(TextBox15.Text)
            rvCmd.Parameters.Add(New OracleParameter("pqty", OracleDbType.Int64)).Value = Integer.Parse(TextBox10.Text)


            ''Dim vStr As String
            ''vStr = rvCmd.ExecuteScalar()
            ''RichTextBox1.Text = vStr
            RichTextBox1.Text = "Added Simple Sale OK"

            'MsgBox("Total number of Tables is " & vStr)
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try

    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged

    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged

    End Sub

    Private Sub TextBox10_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "SUM_CUST_SALESYTD"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "finalval"
            poracle.DbType = DbType.Int32
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("finalval").Value.ToString
            RichTextBox2.Text = vStr

        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox2.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub TextBox16_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            rvCmd.Connection = rvConn
            rvConn.Open()
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "COMMIT"
            rvCmd.ExecuteNonQuery()

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Commit Successful"

        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            rvCmd.Connection = rvConn
            rvConn.Open()
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "ROLLBACK"
            rvCmd.ExecuteNonQuery()

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Rollback Successful!"

        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try

    End Sub

    '' ---------------------------TAS 8.1


    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Try
            Dim connOracle As Oracle.ManagedDataAccess.Client.OracleConnection
            Dim commOracle As New Oracle.ManagedDataAccess.Client.OracleCommand
            Dim paramOracle As Oracle.ManagedDataAccess.Client.OracleParameter
            connOracle = CreateConnection()
            commOracle.Connection = connOracle
            commOracle.CommandType = CommandType.StoredProcedure
            commOracle.CommandText = "PRODDETAILS.GET_ALLPROD_FROM_DB"

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "pReturnValue"
            paramOracle.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor
            paramOracle.Direction = ParameterDirection.Output
            commOracle.Parameters.Add(paramOracle)
            connOracle.Open()

            Dim readerOracle As Oracle.ManagedDataAccess.Client.OracleDataReader
            readerOracle = commOracle.ExecuteReader()
            If readerOracle.HasRows = True Then
                ''Output_TextBox.Text = ""
                Do While readerOracle.Read()
                    MessageBox.Show("Product ID: " & readerOracle("prodid") & "Product Name: " & readerOracle("prodname") & "Selling Price: " & readerOracle("selling_price") & "Sales_YTD: " & readerOracle("sales_ytd"))
                Loop
            Else
                MessageBox.Show("No rows found")
            End If
            connOracle.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Try
            Dim connOracle As Oracle.ManagedDataAccess.Client.OracleConnection
            Dim commOracle As New Oracle.ManagedDataAccess.Client.OracleCommand
            Dim paramOracle As Oracle.ManagedDataAccess.Client.OracleParameter
            connOracle = CreateConnection()
            commOracle.Connection = connOracle
            commOracle.CommandType = CommandType.StoredProcedure
            commOracle.CommandText = "allcustdetails.GET_ALLCUST"

            paramOracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            paramOracle.ParameterName = "pReturnValue"
            paramOracle.OracleDbType = Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor
            paramOracle.Direction = ParameterDirection.Output
            commOracle.Parameters.Add(paramOracle)
            connOracle.Open()

            Dim readerOracle As Oracle.ManagedDataAccess.Client.OracleDataReader
            readerOracle = commOracle.ExecuteReader()
            If readerOracle.HasRows = True Then
                ''Output_TextBox.Text = ""
                Do While readerOracle.Read()
                    MessageBox.Show("Customer ID: " & readerOracle("custid") & "Name: " & readerOracle("custname") & "Status: " & readerOracle("status") & "Sales_YTD: " & readerOracle("sales_ytd"))
                Loop
            Else
                MessageBox.Show("No rows found")
            End If
            connOracle.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim form = New Form2()
        form.Show()
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub
End Class