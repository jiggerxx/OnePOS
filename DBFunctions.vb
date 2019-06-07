Imports MySql.Data.MySqlClient

Module DBFunctions


    Public dbconn As New MySqlConnection("server=localhost;userid=root;password=;database=oneposdb")
    Public cmd As New MySqlCommand
    Public dr As MySqlDataReader

    Public dbconn2 As New MySqlConnection("server=localhost;userid=root;password=;database=oneposdb")
    Public cmd2 As New MySqlCommand
    Public dr2 As MySqlDataReader

    Public dbconn3 As New MySqlConnection("server=localhost;userid=root;password=;database=oneposdb")
    Public cmd3 As New MySqlCommand
    Public dr3 As MySqlDataReader

  
    Public Function checkstate() As Boolean
        Try
            If dbconn.State = ConnectionState.Open Then
                'MessageBox.Show("Database is open!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dbconn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    Public Function loadposnum()

        dbconn.Close()
        Dim CreateCommand As MySqlCommand = dbconn.CreateCommand
        Dim da As New MySqlDataAdapter("SELECT * FROM resibo  ", dbconn)
        Dim dt As New DataTable

        da.Fill(dt)

        pos.TextBox2.Text = Date.Today.ToString("yyyyMMdd") & dt.Rows.Count()

        Return Nothing
    End Function

    Public Function loadtypes()

        Dim typesource As New Dictionary(Of String, String)()
        Dim typeautocomplete As New AutoCompleteStringCollection
        Dim tandf As Boolean = False

        Try

            addproduct.ComboBox3.DataSource = Nothing
            editproduct.ComboBox3.DataSource = Nothing

            checkstate()
            dbconn.Open()

            With cmd
                .Connection = dbconn
                .CommandText = "SELECT * FROM type"
                dr = cmd.ExecuteReader

                While dr.Read

                    typesource.Add(dr.Item("typeID"), dr.Item("type"))
                    typeautocomplete.AddRange(New String() {dr.Item("type")})

                    tandf = True
                End While
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message + "Error1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        dbconn.Close()
        dbconn.Dispose()

        If tandf Then
            Try
                addproduct.ComboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource
                addproduct.ComboBox3.AutoCompleteCustomSource = typeautocomplete
                addproduct.ComboBox3.DataSource = New BindingSource(typesource, Nothing)
                addproduct.ComboBox3.DisplayMember = "Value"
                addproduct.ComboBox3.ValueMember = "Key"
                addproduct.ComboBox3.SelectedIndex = -1

                editproduct.ComboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource
                editproduct.ComboBox3.AutoCompleteCustomSource = typeautocomplete
                editproduct.ComboBox3.DataSource = New BindingSource(typesource, Nothing)
                editproduct.ComboBox3.DisplayMember = "Value"
                editproduct.ComboBox3.ValueMember = "Key"
                editproduct.ComboBox3.SelectedIndex = -1

            Catch ex As Exception
                MessageBox.Show(ex.ToString + " Error in Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("No Records Found! Please Add One First!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return Nothing
    End Function

    Public Function loadproducts()

        Dim prodcomboSource As New Dictionary(Of String, String)()
        Dim prodautocomplete As New AutoCompleteStringCollection
        Dim tandf As Boolean = False

        Try

            editproduct.ComboBox4.DataSource = Nothing
            addstock.ComboBox1.DataSource = Nothing
            'deleteproduct.ComboBox1.DataSource = Nothing

            checkstate()
            dbconn.Open()

            With cmd
                .Connection = dbconn
                .CommandText = "SELECT * FROM products ORDER BY prodname ASC"
                dr = cmd.ExecuteReader

                While dr.Read

                    prodcomboSource.Add(dr.Item("prodcode"), dr.Item("prodname"))
                    prodautocomplete.AddRange(New String() {dr.Item("prodname")})

                    tandf = True
                End While
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message + "Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        dbconn.Close()
        dbconn.Dispose()

        If tandf Then
            Try
                editproduct.ComboBox4.AutoCompleteSource = AutoCompleteSource.CustomSource
                editproduct.ComboBox4.AutoCompleteCustomSource = prodautocomplete
                editproduct.ComboBox4.DataSource = New BindingSource(prodcomboSource, Nothing)
                editproduct.ComboBox4.DisplayMember = "Value"
                editproduct.ComboBox4.ValueMember = "Key"
                editproduct.ComboBox4.SelectedIndex = -1

                addstock.ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
                addstock.ComboBox1.AutoCompleteCustomSource = prodautocomplete
                addstock.ComboBox1.DataSource = New BindingSource(prodcomboSource, Nothing)
                addstock.ComboBox1.DisplayMember = "Value"
                addstock.ComboBox1.ValueMember = "Key"
                addstock.ComboBox1.SelectedIndex = -1

                'deleteproduct.ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
                'deleteproduct.ComboBox1.AutoCompleteCustomSource = prodautocomplete
                'deleteproduct.ComboBox1.DataSource = New BindingSource(prodcomboSource, Nothing)
                'deleteproduct.ComboBox1.DisplayMember = "Value"
                'deleteproduct.ComboBox1.ValueMember = "Key"
                'deleteproduct.ComboBox1.SelectedIndex = -1

            Catch ex As Exception
                MessageBox.Show(ex.ToString + " Error in Products", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            'MessageBox.Show("No Products Found! Please Add One First!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        editproduct.TextBox1.Clear()
        editproduct.TextBox3.Clear()
        editproduct.TextBox2.Clear()
        editproduct.TextBox4.Clear()
        editproduct.ComboBox3.SelectedIndex = -1
        editproduct.ComboBox4.SelectedIndex = -1
        editproduct.RichTextBox1.Clear()

        Return Nothing
    End Function

    Public Function loadsuppliers()
        Dim suppliersource As New Dictionary(Of String, String)()
        Dim supplierautocomplete As New AutoCompleteStringCollection
        Dim tandf As Boolean = False

        Try
            'printbyrangewithsupplier.ComboBox1.DataSource = Nothing
            addstock.ComboBox2.DataSource = Nothing

            checkstate()
            dbconn.Open()

            With cmd
                .Connection = dbconn
                .CommandText = "SELECT * FROM supplier"
                dr = cmd.ExecuteReader

                While dr.Read

                    suppliersource.Add(dr.Item("supplier_code"), dr.Item("supplier_name") + "(" + dr.Item("payment_dues") + " days)")
                    supplierautocomplete.AddRange(New String() {dr.Item("supplier_name") + "(" + dr.Item("payment_dues") + " days)"})

                    tandf = True
                End While
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message + "Error1!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        dbconn.Close()
        dbconn.Dispose()

        If tandf Then
            Try

                addstock.ComboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource
                addstock.ComboBox2.AutoCompleteCustomSource = supplierautocomplete
                addstock.ComboBox2.DataSource = New BindingSource(suppliersource, Nothing)
                addstock.ComboBox2.DisplayMember = "Value"
                addstock.ComboBox2.ValueMember = "Key"
                addstock.ComboBox2.SelectedIndex = -1

                'printbyrangewithsupplier.ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
                'printbyrangewithsupplier.ComboBox1.AutoCompleteCustomSource = supplierautocomplete
                'printbyrangewithsupplier.ComboBox1.DataSource = New BindingSource(suppliersource, Nothing)
                'printbyrangewithsupplier.ComboBox1.DisplayMember = "Value"
                'printbyrangewithsupplier.ComboBox1.ValueMember = "Key"
                'printbyrangewithsupplier.ComboBox1.SelectedIndex = -1

            Catch ex As Exception
                MessageBox.Show(ex.ToString + " Error in Supplier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("No Records Found! Please Add One First!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Return Nothing
    End Function
   End Module
