using System;
using System.Linq;
using System.Collections.Generic;

namespace customTypesLINQlist
{
    public class Bank
    {
        public string BankName { get; set; }
        public int MillionaireCount { get; set; }
    }
    // Build a collection of customers who are millionaires
    public class Customer
    {
        public string CustomerName { get; set; }
        public double Balance { get; set; }
        public string Bank { get; set; }
    }
    public class ReportItem
    {
        public string CustomerName { get; set; }
        public string BankName { get; set; }
    }
    // Define a bank
    public class Banks
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
    public class Program
    {
        public static void Main()
        {
            // Create some banks and store in a List
            List<Banks> banks = new List<Banks>() {
                new Banks(){ Name="First Tennessee", Symbol="FTB"},
                new Banks(){ Name="Wells Fargo", Symbol="WF"},
                new Banks(){ Name="Bank of America", Symbol="BOA"},
                new Banks(){ Name="Citibank", Symbol="CITI"},
            };

            List<Customer> customers = new List<Customer>() {
                new Customer(){ CustomerName="Bob Lesman", Balance=80345.66, Bank="FTB"},
                new Customer(){ CustomerName="Joe Landy", Balance=9284756.21, Bank="WF"},
                new Customer(){ CustomerName="Meg Ford", Balance=487233.01, Bank="BOA"},
                new Customer(){ CustomerName="Peg Vale", Balance=7001449.92, Bank="BOA"},
                new Customer(){ CustomerName="Mike Johnson", Balance=790872.12, Bank="WF"},
                new Customer(){ CustomerName="Les Paul", Balance=8374892.54, Bank="WF"},
                new Customer(){ CustomerName="Sid Crosby", Balance=957436.39, Bank="FTB"},
                new Customer(){ CustomerName="Sarah Ng", Balance=56562389.85, Bank="FTB"},
                new Customer(){ CustomerName="Tina Fey", Balance=1000000.00, Bank="CITI"},
                new Customer(){ CustomerName="Sid Brown", Balance=49582.68, Bank="CITI"}
            };

            IEnumerable<Bank> millionaires = (
                from customer in customers
                group customer by customer.Bank into MoneyBank
                select new Bank
                {
                    BankName = MoneyBank.Key,
                    MillionaireCount = MoneyBank.Where(m => m.Balance >= 1000000).Count()
                }
            );
            // foreach (Bank millionare in millionaires)
            // {
            //     Console.WriteLine($"{millionare.BankName} {millionare.MillionaireCount}");
            // }

            /*
                TASK:
                As in the previous exercise, you're going to output the millionaires,
                but you will also display the full name of the bank. You also need
                to sort the millionaires' names, ascending by their LAST name.

                Example output:
                    Tina Fey at Citibank
                    Joe Landy at Wells Fargo
                    Sarah Ng at First Tennessee
                    Les Paul at Wells Fargo
                    Peg Vale at Bank of America
            */
                List<ReportItem> millionaireReport = (
                                                from aBank in banks
                                                join customer in customers.Where(customer => customer.Balance >= 1000000)
                                                on aBank.Symbol equals customer.Bank
                                                select new ReportItem{
                                                    CustomerName = customer.CustomerName,
                                                    BankName = aBank.Name
                                                }).OrderBy(c => c.CustomerName).ToList();


                foreach (var item in millionaireReport) // item is an object of ReportItem type which has customername and bankname
                {
                    Console.WriteLine($"{item.CustomerName} at {item.BankName}");
                }
            /*
                You will need to use the `Where()`
                and `Select()` methods to generate
                instances of the following class.

                public class ReportItem
                {
                    public string CustomerName { get; set; }
                    public string BankName { get; set; }
                }
            */
        }
    }
}
