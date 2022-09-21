Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class DBUtil
#Region " Database "
    Private Shared _database As Database

    Private Shared _ConnectionString As String = ConfigurationManager.ConnectionStrings("Sample Database Connection String").ConnectionString
    Friend Shared ReadOnly Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
    End Property


    Friend Shared ReadOnly Property Database() As Database
        Get
            If _database Is Nothing Then
                '''''Commented by RDK on April 03, 2014
                'Use this method for faramwork 2.0 and Microsoft.Practices.EnterpriseLibrary 2.0
                '_database = DatabaseFactory.CreateDatabase() 
                'Use this method for faramwork 2.0 and Microsoft.Practices.EnterpriseLibrary 6.0
                _database = New DatabaseProviderFactory().Create("CC Database Connection String")


                '_database = New Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("AMS Database Connection String")
                '_database = DatabaseFactory.SetDatabaseProviderFactory(New DatabaseProviderFactory())
                'DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            End If
            Return _database
        End Get
    End Property
#End Region

#Region " DataSet "

    Public Shared Function ReturnDataSet(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As DataSet
        Try
            Return Database.ExecuteDataSet(spName, parameterValues)
        Catch ex As Exception

        End Try
    End Function




    Public Shared Function ExecuteScalar(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As String
        Try
            Dim ds As DataSet = Database.ExecuteDataSet(spName, parameterValues)
            If ds.Tables(ds.Tables.Count - 1).Rows(0).Item("Status").ToString().ToUpper() = "SUCCESS" Then
                Return CStr(ds.Tables(ds.Tables.Count - 1).Rows(0).Item("Status"))
            Else
                Return CStr(ds.Tables(ds.Tables.Count - 1).Rows(0).Item("Status"))
            End If
            Return ""
            'Return Database.ExecuteScalar(spName, parameterValues)
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function ReturnScalar(ByVal spName As String, ByVal ParamArray parameterValues() As Object) As Object
        Try
            Dim ds As DataSet = Database.ExecuteDataSet(spName, parameterValues)
            If ds.Tables(ds.Tables.Count - 1).Rows(0).Item("Status").ToString().ToUpper() = "SUCCESS" Then
                Return ds.Tables(0).Rows(0).Item(0)
            Else
                Return ds.Tables(0).Rows(0).Item(0)
            End If
            Return ""
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function ReturnDataSetWithCommandTimeoutOLD(ByVal sSqlCommand As SqlClient.SqlCommand, ByVal parameterValues As ArrayList) As DataSet
        Try
            Dim dsDataset As New DataSet
            Dim oEnumerator As IEnumerator = parameterValues.GetEnumerator()
            '//--- Loop through the Parameters in the ArrayList
            Do While (oEnumerator.MoveNext())
                sSqlCommand.Parameters.Add(oEnumerator.Current)
            Loop
            Database.LoadDataSet(sSqlCommand, dsDataset, "Table")

            Return dsDataset
        Catch ex As Exception

        End Try

    End Function
    Public Shared Function ReturnDataSetWithCommandTimeout(ByVal sSqlCommand As SqlClient.SqlCommand, ByVal parameterValues As ArrayList) As DataSet
        Try
            Dim dsDataset As New DataSet
            Dim oEnumerator As IEnumerator = parameterValues.GetEnumerator()
            '//--- Loop through the Parameters in the ArrayList
            Do While (oEnumerator.MoveNext())
                sSqlCommand.Parameters.Add(oEnumerator.Current)
            Loop
            Database.LoadDataSet(sSqlCommand, dsDataset, "Table")

            Return dsDataset
        Catch ex As Exception

        End Try

    End Function
    Public Shared Function ReturnDataSetWithCommandTimeoutWithoutTransaction(ByVal sSqlCommand As SqlClient.SqlCommand, ByVal parameterValues As ArrayList) As DataSet
        Try
            Dim dsDataset As New DataSet
            Dim oEnumerator As IEnumerator = parameterValues.GetEnumerator()
            '//--- Loop through the Parameters in the ArrayList
            Do While (oEnumerator.MoveNext())
                sSqlCommand.Parameters.Add(oEnumerator.Current)
            Loop
            Database.LoadDataSet(sSqlCommand, dsDataset, "Table")

            Return dsDataset
            'Return Database.ExecuteDataSet(spName, parameterValues)
        Catch ex As Exception

        End Try

    End Function
#End Region

End Class
