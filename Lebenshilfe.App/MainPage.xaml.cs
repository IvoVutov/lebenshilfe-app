using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Client_Models.Models;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lebenshilfe.App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void RefreshItems()
        {
            // Verbinden zum MobileService:
            MobileServiceClient mobileServiceClient = new MobileServiceClient(
                  "https://machapp.azure-mobile.net/",
                  "wpLplVeEPPeUkElYJRSPrdrrDBIPDy95"
            );


            // Employee Tabelle laden. 
            IMobileServiceTable<Employee> employeeTable = mobileServiceClient.GetTable<Employee>();
            // Neuen Datensatz anlegen
            Employee em = new Employee() { Firstname = "Sebastian", Lastname = "Küsters" };
            // em in Datenbank speichern
            await employeeTable.InsertAsync(em);
            // Daten aus Tabelle Employee laden
            var empList = await employeeTable.ToCollectionAsync();
            // Neuen Datensatz ändern
            empList.Last().Firstname = "Sebastian Update :)";
            // Geänderten Datensatz speichern 
            await employeeTable.UpdateAsync(empList.Last());
            // Ersten Datensatz löschen 
            await employeeTable.DeleteAsync(empList.First());
            // Aktuellen Daten aus Tabelle laden 
            var updatedList = await employeeTable.ToCollectionAsync();

            // Einfach Breakpoint setzen und die Listen vergleichen ;) 
        }
    }
}
