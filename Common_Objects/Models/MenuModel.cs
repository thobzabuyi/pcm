﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Common_Objects.Models
{
    public class MenuModel
    {
        public Menu GetSpecificMenu(int menuId)
        {
            Menu menu;

            var dbContext = new SDIIS_DatabaseEntities();
            try
            {
                var menusList = (from m in dbContext.Menus
                                 where m.Menu_Id.Equals(menuId) && m.Is_Active == true
                                 select m).ToList();

                menu = (from m in menusList
                        select m).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

            return menu;
        }
        public List<Menu> GetListOfMenus(bool showInActive, bool showDeleted, string SearchDescription)
        {

                List<Menu> menus;

                using (var dbContext = new SDIIS_DatabaseEntities())
                {
                    try
                    {

                        var menusList = (from m in dbContext.Menus
                                         where m.Is_Active.Equals(true) || m.Is_Active.Equals(!showInActive)
                                         where m.Is_Deleted.Equals(false) || m.Is_Deleted.Equals(showDeleted)
                                         select m).ToList();
                    if (SearchDescription != "" && SearchDescription != null)
                    {
                        menusList = (from m in dbContext.Menus
                                         where m.Is_Active.Equals(true) || m.Is_Active.Equals(!showInActive)
                                         where m.Is_Deleted.Equals(false) || m.Is_Deleted.Equals(showDeleted)
                                     where m.Description.Contains(SearchDescription)
                                         select m).ToList();
                    }
                        menus = (from menu in menusList
                                 select menu).ToList();

                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                return menus;
            
        }


        public List<Menu> GetListOfMenus(bool showInActive, bool showDeleted)
        {
            List<Menu> menus;

            using (var dbContext = new SDIIS_DatabaseEntities())
            {
                try
                {
                    var menusList = (from m in dbContext.Menus
                                     where m.Is_Active.Equals(true) || m.Is_Active.Equals(!showInActive)
                                     where m.Is_Deleted.Equals(false) || m.Is_Deleted.Equals(showDeleted)
                                     select m).ToList();

                    menus = (from menu in menusList
                             select menu).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return menus;
        }

        public Menu CreateMenu(string description, bool isActive)
        {
            var dbContext = new SDIIS_DatabaseEntities();

            var menu = new Menu() { Description = description, Is_Active = isActive, Is_Deleted = false };

            try
            {
                var newMenu = dbContext.Menus.Add(menu);

                dbContext.SaveChanges();

                return newMenu;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Menu EditMenu(int menuId, string description)
        {
            var dbContext = new SDIIS_DatabaseEntities();

            try
            {
                var editMenu = (from m in dbContext.Menus
                                where m.Menu_Id.Equals(menuId)
                                select m).FirstOrDefault();

                if (editMenu == null) return null;

                editMenu.Description = description;

                dbContext.SaveChanges();

                return editMenu;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Menu SetMenuIsActive(int menuId, bool isActive)
        {
            var dbContext = new SDIIS_DatabaseEntities();

            try
            {
                var editMenu = (from m in dbContext.Menus
                                where m.Menu_Id.Equals(menuId)
                                select m).FirstOrDefault();

                if (editMenu == null) return null;

                editMenu.Is_Active = isActive;

                dbContext.SaveChanges();

                return editMenu;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Menu SetMenuIsDeleted(int menuId, bool isDeleted)
        {
            var dbContext = new SDIIS_DatabaseEntities();

            try
            {
                var editMenu = (from m in dbContext.Menus
                                where m.Menu_Id.Equals(menuId)
                                select m).FirstOrDefault();

                if (editMenu == null) return null;

                editMenu.Is_Deleted = isDeleted;

                dbContext.SaveChanges();

                return editMenu;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}