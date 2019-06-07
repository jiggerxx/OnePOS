Module Functions

    Public Function home()
        Dim homies As New Panel

        main.Panel3.Controls.Clear()
        homies = Form2.Panel1
        main.Panel3.Controls.Add(homies)

        Return Nothing
    End Function

    Public Function loadpos()
        Dim posies As New Panel

        main.Panel3.Controls.Clear()
        posies = pos.Panel1()
        main.Panel3.Controls.Add(posies)
        pos.TextBox1.Focus()
        pos.DataGridView1.Rows.Clear()
        colordatagridpos()
        Return Nothing
    End Function

    Public Function loadinventory()
        Dim invies As New Panel

        main.Panel3.Controls.Clear()
        invies = inventory.Panel1()
        main.Panel3.Controls.Add(invies)
       
        Return Nothing
    End Function

    Public Function colordatagridpos()

        pos.DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 84, 96)
        pos.DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        pos.DataGridView1.Columns(0).HeaderCell.Style.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        pos.DataGridView1.Columns(1).HeaderCell.Style.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        pos.DataGridView1.Columns(2).HeaderCell.Style.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        pos.DataGridView1.Columns(3).HeaderCell.Style.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        pos.DataGridView1.Columns(4).HeaderCell.Style.Font = New Font("Segoe UI", 11, FontStyle.Regular)
        pos.DataGridView1.EnableHeadersVisualStyles = False

        pos.DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        pos.DataGridView1.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        pos.DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        pos.DataGridView1.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        pos.DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Return Nothing
    End Function

    Public Function posloader()

        pos.TextBox1.Clear()
        pos.DataGridView1.Rows.Clear()
        pos.Label4.Text = "-"
        pos.Label6.Text = "-"

        Return Nothing
    End Function

    Public Function showaddproduct()
        Dim addprod As New Panel

        inventory.Panel3.Controls.Clear()
        addprod = addproduct.Panel1
        inventory.Panel3.Controls.Add(addprod)

        Return Nothing
    End Function

    Public Function showaddstock()
        Dim addstocks As New Panel

        inventory.Panel3.Controls.Clear()
        addstocks = addstock.Panel1
        inventory.Panel3.Controls.Add(addstocks)

        Return Nothing
    End Function

    Public Function showeditprod()
        Dim editprod As New Panel

        inventory.Panel3.Controls.Clear()
        editprod = editproduct.Panel1
        inventory.Panel3.Controls.Add(editprod)

        Return Nothing
    End Function
End Module