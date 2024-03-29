﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace KsiazeczkaZdrowia
{
    [XmlRoot(ElementName = "dog")]
    public class Dog : Animal
    {
        [XmlElement(ElementName = "breed")]
        public string Breed { get; set; }
        [XmlElement(ElementName = "vaccination")]
        public DateTime Vaccination { get; set; }
        [XmlElement(ElementName = "deworming")]
        public DateTime Deworming { get; set; }

        public Dog(string name, DateTime dateOfBirth, string breed):base(name,dateOfBirth)
        {
            this.Breed = breed;
        }

        public Dog() : base()
        {
        }

        public override string ToString()
        {
            string currentData = "Imię: " + Name + "   " + "Rasa: " + Breed + "   " + "Data urodzenia: " + DateOfBirth + "   " + "Waga: " + Weight + "   " + "Szczepienie: " + Vaccination + "   " + "Odrobaczanie: " + Deworming + "   " + "Klinika: " + ClinicName + "   " + "Historia choroby: ";
            foreach (Visit element in MedicalHistory)
            {
                currentData += string.Format("{0}   ", element.Diagnosis);
            }
            return currentData;

        }
        public void EditDog()
        {
            try
            {
                Console.WriteLine("Podaj wagę: ");
                Weight = int.Parse(Console.ReadLine());
                Console.WriteLine("Podaj datę ostatniego szczepienia (rok, miesiąc, dzień) : ");
                Vaccination = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Podaj datę ostatniego odrobaczania (rok, miesiąc, dzień : ");
                Deworming = DateTime.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void LastDates() 
        {
            try
            {
                Console.WriteLine("Ostatnie szczepienie: " + Vaccination);
                Console.WriteLine("Ostatnie odrobaczanie: " + Deworming);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public TimeSpan NextVaccination()
        {
            DateTime today = new DateTime();
            today = DateTime.Now.Date;
            TimeSpan newVaccination = today - Vaccination;
            return newVaccination;
        }
        

    }
}


