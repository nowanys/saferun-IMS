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
using System.Reflection;
namespace SAFERUN.IMS.Web.Services
{
    public class MenuItemService : Service<MenuItem>, IMenuItemService
    {

        private readonly IRepositoryAsync<MenuItem> _repository;
        public MenuItemService(IRepositoryAsync<MenuItem> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<MenuItem> GetByParentId(int parentid)
        {
            return _repository.GetByParentId(parentid);
        }
        public IEnumerable<MenuItem> GetSubMenusByParentId(int parentid)
        {
            return _repository.GetSubMenusByParentId(parentid);
        }







        public IEnumerable<MenuItem> AllMenus()
        {


            return _repository.Queryable();

        }


        public IEnumerable<MenuItem> CreateWithController()
        {
            List<MenuItem> list = new List<MenuItem>();
           
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
            int i=1000;
            string[] actions = new string[] { "Index" };
            foreach (var item in controlleractionlist.Where(x => actions.Contains(x.Action)))
            {
                MenuItem menu = new MenuItem();
                menu.Code = (i++).ToString("0000");
                menu.Description = item.Controller + "/" + item.Action;
                menu.Title = item.Controller.Replace("Controller", "") + "/" + item.Action;
                menu.Url = "/" + item.Controller.Replace("Controller", "") + "/" + item.Action;
                if (!this.Queryable().Where(x => x.Url == menu.Url).Any())
                {
                    this.Insert(menu);
                   
                }

                list.Add(menu);
            }

            return list;
        }
    }
}



