using IBL.BO;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ConsoleUI_BL
{
    partial class Program
    {
        const string ERROR_MESSAGE = "Invalid Option";

        ///// <summary>
        ///// print an item details acording to its type and id
        ///// </summary>
        ///// <param name="type">the type of the item</param>
        ///// <param name="id">the item id</param>
        //private static void printItemById<T>(int id)
        //{
        //    Console.WriteLine(Bl.GetT(id));
        //}

        /// <summary>
        /// activates the main menu
        /// </summary>
        private static void activateMainMenu()
        {
            while (true)
            {
                printTitle("Main Options");
                Console.WriteLine(EnumToString(typeof(MainOption)));
                int input = GetInput(int.Parse);
                switch ((MainOption)input)
                {
                    case MainOption.Add:
                        activateAddMenu();
                        break;
                    case MainOption.Update:
                        activateUpdateMenu();
                        break;
                    case MainOption.Display:
                        activateDisplayMenu();
                        break;
                    case MainOption.DisplayList:
                        activateDisplayListMenu();
                        break;
                    case MainOption.Exit:
                        return;
                    default:
                        Console.WriteLine(ERROR_MESSAGE);
                        break;
                }
            }

        }

        /// <summary>
        /// activates the main menu
        /// </summary>
        private static void activateAddMenu()
        {
            printTitle("Add Options");
            Console.WriteLine(EnumToString(typeof(AddOption), 1));

            int input = GetInput(int.Parse);

            switch ((AddOption)input)
            {
                case AddOption.BaseStation: AddBaseStation(); break;

                case AddOption.Customer: AddCustomer(); break;

                case AddOption.Parcel: AddParcel(); break;

                case AddOption.Drone: AddDrone(); break;

                default: Console.WriteLine(ERROR_MESSAGE); break;
            }
        }

        /// <summary>
        /// activates the display menu
        /// </summary>
        private static void activateDisplayMenu()
        {
            printTitle("Display Options");
            Console.WriteLine(EnumToString(typeof(DisplayOption), 1));

            int option = GetInput(int.Parse);
            int id;

            switch ((DisplayOption)option)
            {
                case DisplayOption.BaseStation:
                    Console.WriteLine("Number Of Station:");
                    id = GetInput(int.Parse);
                    Console.WriteLine(bl.GetBaseStation(id));
                    break;
                case DisplayOption.Customer:
                    Console.WriteLine("Number Of Customer:");
                    id = GetInput(int.Parse);
                    Console.WriteLine(bl.GetCustomer(id));
                    break;
                case DisplayOption.Parcel:
                    Console.WriteLine("Number Of Parcel:");
                    id = GetInput(int.Parse);
                    Console.WriteLine(bl.GetParcel(id));
                    break;
                case DisplayOption.Drone:
                    Console.WriteLine("Number Of Drone:");
                    id = GetInput(int.Parse);
                    Console.WriteLine(bl.GetDrone(id));
                    break;
                default:
                    Console.WriteLine(ERROR_MESSAGE);
                    break;
            }
        }

        /// <summary>
        /// activates the display list menu
        /// </summary>
        private static void activateDisplayListMenu()
        {
            printTitle("Display List Options");
            Console.WriteLine(EnumToString(typeof(DisplayListOption), 1));

            int option = GetInput(int.Parse);

            switch ((DisplayListOption)option)
            {
                case DisplayListOption.BaseStation:
                    DisplayList(bl.GetBaseStationsList());
                    break;
                case DisplayListOption.Customer:
                    DisplayList(bl.GetCustomersList());
                    break;
                case DisplayListOption.Parcel:
                    DisplayList(bl.GetParcelsList());
                    break;
                case DisplayListOption.Drone:
                    DisplayList(bl.GetDronesList());
                    break;
                case DisplayListOption.NotAssignedToDroneParcels:
                    DisplayList(bl.GetNotAssignedToDroneParcels());
                    break;
                case DisplayListOption.AvailableBaseStations:
                    DisplayList(bl.GetAvailableBaseStations());
                    break;
                default:
                    Console.WriteLine(ERROR_MESSAGE);
                    break;
            }
        }

        /// <summary>
        /// activates the update menu
        /// </summary>
        private static void activateUpdateMenu()
        {
            printTitle("Update Options");
            Console.WriteLine(EnumToString(typeof(UpdateOption), 1));

            int option = GetInput(int.Parse);

            switch ((UpdateOption)option)
            {
                case UpdateOption.AssignParcelToDrone:
                    {
                        Console.WriteLine("Parcel ID, Please.");
                        int parcelId = GetInput(int.Parse);
                        bl.AssignParcelToDrone(parcelId);
                        break;
                    }
                case UpdateOption.CollectParcel:
                    {
                        Console.WriteLine("Parcel ID, Please.");
                        int parcelId = GetInput(int.Parse);
                        bl.PickUpParcel(parcelId);

                        break;
                    }
                case UpdateOption.SupplyParcel:
                    {
                        Console.WriteLine("Parcel ID, Please.");
                        int parcelId = GetInput(int.Parse);
                        bl.SupplyParcel(parcelId);

                        break;
                    }
                case UpdateOption.ChargeDroneAtBaseStation:
                    {
                        Console.WriteLine("Drone ID, Please.");
                        int droneId = GetInput(int.Parse);
                        bl.SendDroneToCharge(droneId);

                        break;
                    }
                case UpdateOption.FinishCharging:
                    {
                        Console.WriteLine("Drone ID, Please.");
                        int droneId = GetInput(int.Parse);
                        Console.WriteLine("Time in Charge:");
                        double timeInCharge = GetInput(double.Parse);
                        bl.FinishCharging(droneId, timeInCharge);

                        break;
                    }

                case UpdateOption.RenameDrone:
                    {
                        Console.WriteLine("Drone ID, Please.");
                        int droneId = GetInput(int.Parse);
                        Console.WriteLine("New Name:");
                        string name = GetInput(s => s);
                        bl.RenameDrone(droneId, name);

                        break;
                    }

                case UpdateOption.UpdateBaseStation:
                    {
                        Console.WriteLine("Drone ID, Please.");
                        int droneId = GetInput(int.Parse);
                        Console.WriteLine("BaseStation Name:");
                        string name = GetInput(s => s); 
                        Console.WriteLine("Charge Slots:");
                        int? chargeSlots = GetInput(int.Parse);

                        name = name.Count() == 0 ? null : name;
                        chargeSlots = chargeSlots == 0? null : chargeSlots;
                        bl.RenameDrone(droneId, name);

                        break;
                    }
                case UpdateOption.UpdateCustomer:
                    {
                        Console.WriteLine("Customer ID, Please.");
                        int customerId = GetInput(int.Parse);
                        Console.WriteLine("New Name:");
                        string name = GetInput(s => s);
                        Console.WriteLine("Phone number:");
                        string phone = GetInput(s => s);

                        name = name.Count() == 0 ? null : name;
                        phone = phone.Count() == 0 ? null : phone;
                        bl.UpdateCustomer(customerId, name, phone);

                        break;
                    }
                default:
                    Console.WriteLine(ERROR_MESSAGE);
                    break;
            }
        }

        /// <summary>
        /// prints a list of items
        /// </summary>
        /// <param name="list">the list to print</param>
        private static void DisplayList(IEnumerable list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}