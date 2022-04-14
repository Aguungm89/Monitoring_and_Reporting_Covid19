using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonitoringandReportingCOVID19.Models;

namespace MonitoringandReportingCOVID19.viewmodel
{
    public class ViMoviewmodel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Temp_Employee> Temp_Employees { get; set; }
        public IEnumerable<CovidConfirmCas> CovidConfirmCas { get; set; }
        public IEnumerable<V_ConfirmEMP> V_ConfirmEMPs { get; set; }
        public IEnumerable<V_ActiveCases> V_ActiveCases { get; set; }
        public IEnumerable<V_RecoveryCases> V_RecoveryCases  { get; set; }
        public IEnumerable<V_DeathCases> V_DeathCases { get; set; }
        public IEnumerable<V_Uploadnew> V_Uploadnews { get; set; }
        public IEnumerable<V_Uploadold> V_Uploadolds { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Employee_location> Employee_Locations { get; set; }
    }
}