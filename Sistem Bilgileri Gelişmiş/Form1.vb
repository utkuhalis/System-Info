Imports System.Management
Public Class Form1

    Private Sub islemci()
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            ListBox1.Items.Add("İşlemci: " & info("manufacturer").ToString())
            ListBox1.Items.Add("İşlemci Name: " & info("name").ToString())
            ListBox1.Items.Add("İşlemci Model: " & info("caption").ToString())
            ListBox1.Items.Add("İşlemci Version: " & info("version").ToString())
        Next
    End Sub

    Private Sub bıos()
        Dim query As New SelectQuery("Win32_bios")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            ListBox1.Items.Add("Bios Name: " & info("name").ToString())
            ListBox1.Items.Add("Bios Version: " & info("version").ToString())
            ListBox1.Items.Add("Bios: " & info("manufacturer").ToString())
        Next
    End Sub

    Private Sub harddisk()
        Dim disk As New ManagementClass("Win32_PhysicalMedia")
        For Each Hdisk As ManagementObject In disk.GetInstances()
            If Hdisk("SerialNumber") <> Nothing Then
                ListBox1.Items.Add("Hard Disk Serial: " & CStr(Hdisk("serialnumber")))
            End If
        Next Hdisk
    End Sub

    Private Sub cpuu()
        Try
            Dim searcher As New ManagementObjectSearcher("root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature")
            For Each queryObj As ManagementObject In searcher.Get()
                Dim temperature As Double = CDbl(queryObj("CurrentTemperature"))
                temperature = (temperature - 2732) / 10.0
                ListBox1.Items.Add("Cpu: " & temperature.ToString & " °C")
            Next
        Catch
            ListBox1.Items.Add("Cpu: Error Cpu °C")
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        islemci()
        bıos()
        harddisk()
        cpuu()
        ListBox1.Items.Add("Operating System: " & My.Computer.Info.OSFullName)
        ListBox1.Items.Add("Operating System Version: " & My.Computer.Info.OSVersion)
        ListBox1.Items.Add("Time: " & Now())
    End Sub
End Class
