Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports Oracle.ManagedDataAccess.Client

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ''cust id for 4.1
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        ''prod id for 4.1
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        ''qty for 4.1
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        ''date for 4.1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        ''Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        ''rvCmd.BindByName = True

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "ADD_COMPLEX_SALE_TO_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pcustid", OracleDbType.Int64)).Value = Integer.Parse(TextBox1.Text)

            rvCmd.Parameters.Add(New OracleParameter("pprodid", OracleDbType.Int64)).Value = Integer.Parse(TextBox2.Text)
            rvCmd.Parameters.Add(New OracleParameter("pqty", OracleDbType.Int64)).Value = Integer.Parse(TextBox3.Text)
            rvCmd.Parameters.Add(New OracleParameter("pdate", OracleDbType.NVarchar2)).Value = TextBox4.Text

            ''rvCmd.Parameters.Add(New OracleParameter("pdate", OracleDbType.NVarchar2)).Value = TextBox4.Text


            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Added Complex Sale OK"

        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "COUNT_PRODUCT_SALES_FROM_DB"
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "totalcount"
            poracle.DbType = DbType.String
            poracle.Size = 200
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            ''rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox6.Text)
            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "pdays"
            poracle.DbType = DbType.Int32
            poracle.Value = Integer.Parse(TextBox5.Text)
            ''poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvConn.Open()

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("totalcount").Value.ToString
            ''RichTextBox1.Text = "Customer details :"
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_SALE_FROM_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure

            poracle = New Oracle.ManagedDataAccess.Client.OracleParameter
            poracle.ParameterName = "smallestval"
            poracle.DbType = DbType.Int32
            poracle.Direction = ParameterDirection.ReturnValue
            rvCmd.Parameters.Add(poracle)

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            vStr = rvCmd.Parameters.Item("smallestval").Value.ToString
            RichTextBox1.Text = "Smallest Sale Deleted"
            RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Dim poracle As Oracle.ManagedDataAccess.Client.OracleParameter

        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_ALL_SALES_FROM_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure

            rvCmd.ExecuteNonQuery()
            Dim vStr As String
            ''vStr = rvCmd.Parameters.Item("rCount").Value.ToString
            RichTextBox1.Text = "All Products Deleted. Number :"
            '' RichTextBox1.Text = vStr


        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr

        Finally
            rvConn.Close()

        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_CUSTOMER"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox6.Text)

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Customer Deleted OK"

            'MsgBox("Total number of Tables is " & vStr)
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim rvConn As Oracle.ManagedDataAccess.Client.OracleConnection
        rvConn = CreateConnection()
        Dim rvCmd As New Oracle.ManagedDataAccess.Client.OracleCommand
        Try
            RichTextBox1.Clear()
            rvCmd.Connection = rvConn
            rvCmd.CommandText = "SET SERVEROUTPUT ON"
            rvCmd.CommandText = "DELETE_PROD_FROM_DB"
            rvConn.Open()
            rvCmd.CommandType = CommandType.StoredProcedure
            rvCmd.Parameters.Add(New OracleParameter("pCustId", OracleDbType.Int64)).Value = Integer.Parse(TextBox7.Text)

            Dim vStr As String
            vStr = rvCmd.ExecuteScalar()
            RichTextBox1.Text = vStr
            RichTextBox1.Text = "Product Deleted OK"

            'MsgBox("Total number of Tables is " & vStr)
        Catch ex As Exception
            Dim nstr As String
            nstr = ex.Message
            RichTextBox1.Text = nstr
        Finally
            rvConn.Close()
        End Try
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim form = New Form1()
        form.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim connOracle As Oracle.ManagedDataAccess.Client.OracleConnection
            Dim commOracle As New Oracle.ManagedDataAccess.Client.OracleCommand
            Dim paramOracle As Oracle.ManagedDataAccess.Client.OracleParameter
            connOracle = CreateConnection()
            commOracle.Connection = connOracle
            commOracle.CommandType = CommandType.StoredProcedure
            commOracle.CommandText = "allsales.GET_ALLSALES_FROM_DB"

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
                    MessageBox.Show("Sale ID: " & readerOracle("saleid") & "Customer ID: " & readerOracle("custid") & "Product ID " & readerOracle("prodid") & "Date: " & readerOracle("saledate") & "Amount: " & readerOracle("price"))
                Loop


            Else
                MessageBox.Show("No rows found")
            End If
            connOracle.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class


