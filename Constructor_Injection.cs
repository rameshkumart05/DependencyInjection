using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Injection
{
    // Interview question : How to call two interface having same method name ? Ans : Using Dependency Injections

    // Definition of DI
    //The process of injecting(converting) coupled(dependent) objects into decoupled(independent) objects is called Dependency Injection.
    /*
      Types of Dependency Injection:
    There are four types of DI:

    1.Constructor Injection
    2.Setter Injection
    3.Interface-based injection
    4.Service Locator Injection
      */

    //Service Interface
    public interface IService
    {
        string ServiceMethod();
    }
    public interface IService1
    {
        string ServiceMethod();
    }
    //Service Interface implementation for Claim Service
    public class LatePaymentService : IService1
    {
        public string ServiceMethod()
        {
            return "LatePaymentService is running..";
        }
    }

    //Service Interface implementation for Claim Service
    public class ClaimService : IService
    {
        public string ServiceMethod()
        {
            return "ClaimService is running..";
        }
    }

    //Service Interface implementation for Adjudication Service
    public class AdjudicationService : IService
    {
        public string ServiceMethod()
        {
            return "AdjudicationService is running..";
        }
    }
    //Service Interface implementation for Payment Service
    public class PaymentService : IService
    {
        public string ServiceMethod()
        {
            return "PaymentService is running..";
        }
    }


    public class BusinessLogicImplementation
    {
        //Constructor Injection Implementation for all services

        //Implementing Getter and Setter Injection

        private IService _client;
        private IService1 client1;
        public IService Client
        {
            get { return _client; }
            set { _client = value; }
        }
        public IService1 Client1
        {
            get { return client1; }
            set { client1 = value; }
        }

        public void testSetterInj_I1()
        {
            Console.WriteLine("Constructor Injection Injection ==> Current Service: {0}", Client.ServiceMethod());
        }
        public void testSetterInj_I2()
        {
            Console.WriteLine("Constructor Injection Injection ==> Current Service: {0}", Client1.ServiceMethod());
        }
        /*public BusinessLogicImplementation(IService client,Iservice1 client1)
        {
            this._client = client;
            this.client = client1;
            Console.WriteLine("Constructor Injection Injection ==> Current Service: {0}", client.ServiceMethod());
            Console.WriteLine("Constructor Injection Injection ==> Current Service: {0}", client1.ServiceMethod());

        }*/
    }

//Support interface
interface ISetService
{
        void setIServiceRunService(IService client);
}
//Implementing Interface Injection  
public class BusinessLogicImplementationInterfaceDI : ISetService
{
        IService _client1;
   public void setIServiceRunService(IService client)
   {
       _client1 = client;
            //Console.WriteLine(_client1.ServiceMethod());

            Console.WriteLine("Interface Injection ==> Current ServiceInterfaceDI : {0}", _client1.ServiceMethod());
   }
}

    //Implementing Service Locator Injection
    public class BusinessLogicServiceLocatorImplementation
    {
        private static IService _clientLocator;

        public static IService getService()
        {
            return _clientLocator;
        }

        public static void setService(IService clientSL)
        {
            _clientLocator = clientSL;
        }
    }
    internal class Constructor_Injection    
    {
        public static void Main()
        {
            // Consuming Constructor Injection
            //BusinessLogicImplementation conInjBusinessLogic = new BusinessLogicImplementation(new ClaimService(),new LatePaymentService());
            //Consuming Getter and Setter Injection
            BusinessLogicImplementation ConInjBusinessLogic = new BusinessLogicImplementation();
            ConInjBusinessLogic.Client = new ClaimService();
            ConInjBusinessLogic.testSetterInj_I1();
            ConInjBusinessLogic.Client1 = new LatePaymentService();
            ConInjBusinessLogic.testSetterInj_I2();

            //Consuming Interface Injection

            BusinessLogicImplementationInterfaceDI objInterfaceDI = new BusinessLogicImplementationInterfaceDI();
            // serviceObj = new ClaimService();
            objInterfaceDI.setIServiceRunService(new AdjudicationService());

            // Consuming Service Locator Injection
            BusinessLogicServiceLocatorImplementation.setService(new PaymentService());
            IService client2 = BusinessLogicServiceLocatorImplementation.getService();
            Console.WriteLine("Service Locator => Current Service : {0}",client2.ServiceMethod());

            Console.ReadKey();
  
        }
    }
}
