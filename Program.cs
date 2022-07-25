﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Twilio.TwiML;
using Bond;
using NPOI.SS.Formula.Functions;
using System.Linq;

namespace KsiazeczkaZdrowia
{
    class Program
    {
        static List<Dog> listOfDogs = new List<Dog>();
        static List<Cat> listOfCats = new List<Cat>();
        static List<Clinic> listOfClinics = new List<Clinic>();
        static List<Visit> medicalHistory = new List<Visit>();

        static void Main(string[] args)
        {
            listOfDogs = GetDogs();
            listOfCats = GetCats();
            listOfClinics = GetClinics();
            SaveData();
            HandlingOfMenu();
        }
        public static List<Clinic> GetClinics()
        {
            Clinic animal = new Clinic("ANIMAL", "aaa");
            Clinic kaczor = new Clinic("KACZOR", "kkk");
            
            listOfClinics.Add(animal);
            listOfClinics.Add(kaczor);

            return listOfClinics;
        }

        public static List<Dog> GetDogs()
        {
            Dog blacky = new Dog("BLACKY", new DateTime(2018, 05, 17), "Wyżeł Małopolski");
            blacky.Weight = 20;
            blacky.Vaccination = new DateTime(2021, 10, 01);
            blacky.Deworming = new DateTime(2021, 07, 28);
            blacky.MedicalHistory = new List<Visit> ();
            blacky.ClinicName = listOfClinics[0].Name;

            Dog lucciano = new Dog("LUCCIANO", new DateTime(2015, 12, 24), "Lhasa Apso");
            lucciano.ClinicName = listOfClinics[1].Name;

            Dog kaprys = new Dog("KAPRYS", new DateTime(2016, 02, 01), "Lhasa Apso");
            kaprys.ClinicName = listOfClinics[1].Name;

            listOfDogs.Add(blacky);
            listOfDogs.Add(lucciano);
            listOfDogs.Add(kaprys);

            return listOfDogs;
        }

        public static List<Cat> GetCats()
        {
            Cat diesel = new Cat("DIESEL", new DateTime(2008, 01, 01), "Archangielska");
            diesel.ClinicName = listOfClinics[1].Name;

            listOfCats.Add(diesel);

            return listOfCats;   
        }

        public static void SaveData()
        {
            string pDog = @"listaPsow.txt";
            string pCat = @"listaKotow.txt";
            string pClinic = @"listaKlinik.txt";

            SaveXml.SaveDataXml(listOfClinics, pClinic);
            SaveXml.SaveDataXml(listOfCats, pCat);
            SaveXml.SaveDataXml(listOfDogs, pDog);
        }

        private static int ShowMenu()
        {
            Console.WriteLine($"Spis treści: { Environment.NewLine} 1.Pacjenci { Environment.NewLine} 2.Dodaj pacjenta { Environment.NewLine} 3.Najbliższe terminy { Environment.NewLine} 4.Kliniki {Environment.NewLine} 5.Zakończ ");
            
            try
            {
                int choice = int.Parse(Console.ReadLine());
                return choice;
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                int bug = 0;
                HandlingOfMenu();
                return bug;
            }
        }

        private static void HandlingOfMenu()
        {
            try
            {
                int choice = ShowMenu();

                switch (choice)
                {
                    case 1:
                        SelectPatient();
                        break;

                    case 2:
                        Console.WriteLine("Jeżeli chcesz dodać psa wybierz '1', aby dodać kota - wybierz '2' ");
                        int selectAnimal = int.Parse(Console.ReadLine());
                        if (selectAnimal == 1)
                        {
                        AddDog();
                        }
                        else if (selectAnimal == 2)
                        {
                        AddCat();
                        }
                        else
                        {
                        HandlingOfMenu();
                        }
                        break;

                    case 3:
                        NextDates();
                        break;

                    case 4:
                        SelectClinic();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ShowMenu();
            }
        }
        private static int SelectAnimal()
        {
            Console.WriteLine($"Lista pacjentów: {Environment.NewLine} 1.Psy: {Environment.NewLine} 2.Koty: ");
            int selectAnimal = int.Parse(Console.ReadLine());

            if (selectAnimal == 1)
            {
                ShowListOfDogs();
                Console.WriteLine($"Aby zobaczyć profil pacjenta wpisz jego imię{Environment.NewLine} 0.Powrót");
            }
            else if (selectAnimal == 2)
            {
                ShowListOfCats();
                Console.WriteLine($"Aby zobaczyć profil pacjenta wpisz jego imię{Environment.NewLine} 0.Powrót");
            }
            else
            {
                HandlingOfMenu();
            }

            return selectAnimal;
        }

        private static void ShowListOfDogs() 
        {
            foreach (Dog element in listOfDogs)
            {
                Console.WriteLine(element.Name);
            }
        }

        private static void ShowListOfCats() 
        {
            foreach (Cat element in listOfCats)
            {
                Console.WriteLine(element.Name);
            }
        }

        private static void SelectPatient()
        {
            int selectAnimal = SelectAnimal();

            string name = Console.ReadLine();
            string patientsName = name.ToUpper();

            switch (patientsName)
            {
                case "BLACKY":

                    listOfDogs[0].WriteData();
                    DogsMenu();

                    break;

                case "KAPRYS":

                    listOfDogs[2].WriteData();
                    DogsMenu();

                    break;

                case "LUCCIANO":

                    listOfDogs[1].WriteData();
                    DogsMenu();

                    break;

                case "DIESEL":

                    listOfCats[0].WriteData();
                    CatsMenu();

                    break;

                default:
                    SelectPatient();
                    break;
            }
        }

        private static void DogsMenu() 
        {
            int choice;

            do
            {
                Console.WriteLine($"1. Dodaj wpis do historii choroby {Environment.NewLine} 2.Aktualizuj dane {Environment.NewLine} 0.Powrót");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    listOfDogs[0].AddsVisit();
                }
                else if (choice == 2)
                {
                    listOfDogs[0].EditDog();
                }
                else if (choice == 0)
                {
                    HandlingOfMenu();
                }
                else
                {
                    Console.WriteLine($"1. Dodaj wpis do historii choroby {Environment.NewLine} 2.Aktualizuj dane {Environment.NewLine} 0.Powrót");
                }

            } while (choice != 0);
        }

        private static void CatsMenu() 
        {
            int choice;

            do
            {
                Console.WriteLine($"1. Dodaj wpis do historii choroby {Environment.NewLine} 2.Aktualizuj dane {Environment.NewLine} 0.Powrót");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    listOfCats[0].AddVisit();
                }
                else if (choice == 2)
                {
                    listOfCats[0].EditCat();
                }
                else if (choice == 0)
                {
                    HandlingOfMenu();
                }
                else
                {
                    Console.WriteLine($"1. Dodaj wpis do historii choroby {Environment.NewLine} 2.Aktualizuj dane {Environment.NewLine} 0.Powrót");
                }

            } while (choice != 0);
        }
        
        public static Dog AddDog()
        {
            Console.WriteLine("Podaj imię psa: ");
            string nName = Console.ReadLine();
            Console.WriteLine("Podaj rasę: ");
            string nBreed = Console.ReadLine();
            Console.WriteLine("Podaj datę urodzenia psa: ");
            DateTime nDateOfBirth = new DateTime();
            nDateOfBirth = DateTime.Parse(Console.ReadLine());

            Dog x = new Dog(nName, nDateOfBirth, nBreed);

            Console.WriteLine("Jeżeli chcesz uzupełnić dodatkowe dane pacjenta (np. waga, szczepienie) wybierz '1', aby powrócić do spisu treści wybierz '0'  ");
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 1)
            {
                Console.WriteLine("Podaj wagę: ");
                int nWeight = int.Parse(Console.ReadLine());
                x.Weight = nWeight;
                Console.WriteLine("Podaj nazwe kliniki: ");
                string nameOfClinic = Console.ReadLine();
                x.ClinicName = nameOfClinic;
                Console.WriteLine("Podaj datę ostatniego szczepienia (rok, miesiąc, dzień) : ");
                DateTime nVaccination = new DateTime();
                nVaccination = DateTime.Parse(Console.ReadLine());
                x.Vaccination = nVaccination;
                Console.WriteLine("Podaj datę ostatniego odrobaczania : ");
                DateTime nDeworming = new DateTime();
                nDeworming = DateTime.Parse(Console.ReadLine());
                x.Deworming = nDeworming;
            }
            else
            {
                HandlingOfMenu();
            }

            return x;
        }

        public static Cat AddCat()
        {
            Console.WriteLine("Podaj imię kota: ");
            string nName = Console.ReadLine();
            Console.WriteLine("Podaj rasę: ");
            string nBreed = Console.ReadLine();
            Console.WriteLine("Podaj datę urodzenia kota: ");
            DateTime nDateOfBirth = new DateTime();
            nDateOfBirth = DateTime.Parse(Console.ReadLine());

            Cat y = new Cat(nName, nDateOfBirth, nBreed);
            listOfCats.Add(y);

            Console.WriteLine("Jeżeli chcesz uzupełnić dodatkowe dane pacjenta (np. waga, szczepienie) wybierz '1', aby powrócić do spisu treści wybierz '0'  ");
            int choice = int.Parse(Console.ReadLine());
            
            if (choice == 1)
            {
                Console.WriteLine("Podaj wagę: ");
                int nWeigth = int.Parse(Console.ReadLine());
                y.Weight = nWeigth;
                Console.WriteLine("Podaj nazwe kliniki: ");
                string nameOfClinic = Console.ReadLine();
                y.ClinicName = nameOfClinic;
                Console.WriteLine("Podaj datę ostatniego odrobaczania : ");
                DateTime nDeworming = new DateTime();
                nDeworming = DateTime.Parse(Console.ReadLine());
                y.Deworming = nDeworming;
            }
            else
            {
             HandlingOfMenu();
            }

            return y;
        }
        
        private static void NextDates()
        { 
            Console.WriteLine("Najbliższe szczepienia: ");
            Console.WriteLine("BLACKY " + listOfDogs[0].NextVaccination());
            Console.WriteLine("LUCCIANO " + listOfDogs[1].NextVaccination());
            Console.WriteLine("KAPRYS" + listOfDogs[2].NextVaccination());
            Console.WriteLine("DIESEL" + listOfCats[0].NextVaccination());

            HandlingOfMenu();
        }

        private static void SelectClinic()
        {
            ShowListOfClinics();
            Console.WriteLine($"Aby zobaczyć profil kliniki podaj jej nazwę, jeżeli chcesz dodać nową klinikę wpisz 'nowa' {Environment.NewLine} Lista klinik: ");
            
            try
            {
                string name = Console.ReadLine();
                string nameOfClinic = name.ToUpper();

                try
                {
                    switch (nameOfClinic)
                    {
                        case "ANIMAL":
                            
                            listOfClinics[0].WriteData();
                            ClinicsMenu();
                            break;

                        case "KACZOR":

                            listOfClinics[1].WriteData();
                            ClinicsMenu();
                            break;

                        case "NOWA":
                                    
                            AddClinic();
                            break;
                    }
                                                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    SelectClinic();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                HandlingOfMenu();
            }
        }

        private static void ShowListOfClinics() 
        {
            foreach (Clinic element in listOfClinics)
            {
                Console.WriteLine(element.Name);
            }
        }

        private static void ClinicsMenu() 
        {
            int choice;
            do
            {
                Console.WriteLine($"Co chcesz zrobic? {Environment.NewLine} 1. Edytuj dane kliniki {Environment.NewLine} 0.Powrót");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    listOfClinics[0].EditClinicData();
                }
                else if (choice == 0)
                {
                    SelectClinic();
                }
                else
                {
                    HandlingOfMenu();
                }
            } while (choice != 0);
        }
       private static Clinic AddClinic()
        {
            Console.WriteLine("Podaj nazwę kliniki");
            string nName = Console.ReadLine();
            Console.WriteLine("Podaj adres: ");
            string nAddress = Console.ReadLine();
            Console.WriteLine("Podaj numer kontaktowy: ");
            string nContact = Console.ReadLine();
            Console.WriteLine("Podaj usługi weterynaryjne: ");
            string nService = Console.ReadLine();
            Console.WriteLine("Dodaj lekarza weterynarii");
            string nDoctor = Console.ReadLine();

            Clinic z = new Clinic(nName, nAddress);
            z.Contact.Add(nContact);
            z.Service.Add(nService);
            z.Doctors.Add(nDoctor);

            return new Clinic(nName, nAddress);

        }


    }
}




