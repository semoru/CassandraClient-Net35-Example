Imports CassandraSharp
Imports System.Text
Imports CassandraSharp.Config
Imports CassandraSharp.MadeSimple
Imports Apache.Cassandra

Module Main

    Sub Main()

        ' Loading Cassandra configuration...
        XmlConfigurator.Configure()
        Dim cluster As ICluster = ClusterManager.GetCluster("TestCassandra")

        ' Define your query
        Dim queryString As String = "select * from table"

        'Execute your query in Cassandra
        Dim res As CqlResult = cluster.ExecuteCql(queryString)

        ' Connection release
        ClusterManager.Shutdown()

        ' Get results. In this example integer, date and double types
        '
        'getId(res.Rows.ElementAt(i).Columns.ElementAt(0).Value
        'getDate(res.Rows.ElementAt(i).Columns.ElementAt(j).Name)
        'getDouble(res.Rows.ElementAt(i).Columns.ElementAt(j).Value))  

    End Sub


    Public Function getId(ByVal value As Byte()) As Integer
        Dim buffer As Byte() = New Byte(value.Length - 1) {}
        value.CopyTo(buffer, 0)
        Array.Reverse(buffer)
        Dim result As Integer = BitConverter.ToInt32(buffer, 0)
        Return result
    End Function

    Public Function getDate(ByVal value As Byte()) As DateTime
        Dim buffer As Byte() = New Byte(value.Length - 1) {}
        value.CopyTo(buffer, 0)
        Array.Reverse(buffer)
        Dim ticks As Long = BitConverter.ToInt64(buffer, 0)
        Dim dateTime As New System.DateTime(1970, 1, 1, 0, 0, 0, _
             0)

        dateTime = dateTime.AddMilliseconds(ticks)
        Return dateTime
    End Function

    Public Function getDouble(ByVal value As Byte()) As Double
        Dim buffer As Byte() = New Byte(value.Length - 1) {}
        value.CopyTo(buffer, 0)
        Array.Reverse(buffer)
        Dim result As Double = BitConverter.ToDouble(buffer, 0)
        Return result
    End Function




End Module
