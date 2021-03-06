﻿

     
 
 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repository.Pattern.Repositories;
using Service.Pattern;
using SAFERUN.IMS.Web.Models;
using SAFERUN.IMS.Web.Repositories;
using System.Data;

namespace SAFERUN.IMS.Web.Services
{
    public interface IProcessStepService:IService<ProcessStep>
    {

                  IEnumerable<ProcessStep> GetByStationId(int  stationid);
                 IEnumerable<ProcessStep> GetByProductionProcessId(int  productionprocessid);
        
         
 
		void ImportDataTable(DataTable datatable);
	}
}