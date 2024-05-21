using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CrystalDecisions;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;

/// <summary>
/// Summary description for ReportFactory
/// </summary>
public class ReportFactory
{
	public ReportFactory()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected static Queue reportQueue = new Queue();
    protected static int iMaxCount = 5;

    protected static ReportDocument CreateReport(Type reportClass)
    {
        object report = Activator.CreateInstance(reportClass);
        reportQueue.Enqueue(report);
        return (ReportDocument)report;
    }

    public static ReportDocument GetReport(Type reportClass)
    {
        if (reportQueue.Count > iMaxCount)
        {
            ((ReportDocument)reportQueue.Dequeue()).Close();
            ((ReportDocument)reportQueue.Dequeue()).Dispose();
            GC.Collect();
        }
        return CreateReport(reportClass);
    } 
}
