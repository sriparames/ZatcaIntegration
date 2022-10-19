using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZatcaIntegrationSDK;
using ZatcaIntegrationSDK.BLL;
using ZatcaIntegrationSDK.HelperContracts;

namespace ZatcaIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_SimplifiedInvoice_Click(object sender, EventArgs e)
        {
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)




            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2022-08-17";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 388;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0200000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "123654";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Payment Notes"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى

            //بيانات البائع 
           inv.SupplierParty.partyIdentification.ID = "123456";
           inv.SupplierParty.partyIdentification.schemeID = "CRN"; //رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "manofthematch";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "makka";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "مكة المكرمة";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "flassk";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName ="الأنظمة المتحدة الأمنية";
            inv.SupplierParty.partyTaxScheme.CompanyID = "300300868600003";
            
            // بيانات المشترى
            inv.CustomerParty.partyIdentification.ID = "123456";
            inv.CustomerParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";
            inv.CustomerParty.postalAddress.AdditionalStreetName = "";
            inv.CustomerParty.postalAddress.BuildingNumber = "3724";
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";



            AllowanceCharge allowancecharge = new AllowanceCharge();

            allowancecharge.Amount = 2;
            allowancecharge.AllowanceChargeReason = "discount"; //reason
            allowancecharge.taxCategory.ID = "S";
            allowancecharge.taxCategory.Percent = 15;
            inv.allowanceCharges.Add(allowancecharge);

            // inv.legalMonetaryTotal.PrepaidAmount = 10;
            for (int i = 0; i < 3; i++)
            {
                InvoiceLine invline = new InvoiceLine();
                invline.InvoiceQuantity = 44;


                invline.item.Name = "Computer"+i.ToString();
               
                    invline.item.classifiedTaxCategory.ID = "S";
                    invline.item.classifiedTaxCategory.Percent = 15;
               
                  

                invline.price.PriceAmount = 25+i;
                invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
                invline.price.allowanceCharge.Amount = 2;
                TaxSubtotal taxsub = new TaxSubtotal();

               
                    taxsub.taxCategory.ID = "S";
                    taxsub.taxCategory.Percent = 15;
                   
                    invline.taxTotal.TaxSubtotal.Add(taxsub);
               

                inv.InvoiceLines.Add(invline);
            }
            


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
             
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;

                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ReportingStatus); //REPORTED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
        } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
}

        private void btn_StandaredInvoice_Click(object sender, EventArgs e)
        {
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)
            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2021-01-05";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 388;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0100000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "123654";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Payment Notes"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى
            //بيانات البائع
            inv.SupplierParty.partyIdentification.ID = "123456";
            inv.SupplierParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "Jeddah";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.SupplierParty.partyTaxScheme.CompanyID = "310175397400003";
            // بيانات المشترى اجبارى
            inv.CustomerParty.partyIdentification.ID = "123456";//رقم السجل التجارى
            inv.CustomerParty.partyIdentification.schemeID = "CRN";
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.CustomerParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.CustomerParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";

            AllowanceCharge allowancecharge = new AllowanceCharge();

            allowancecharge.Amount = 2;
            allowancecharge.AllowanceChargeReason = "discount"; //reason
            allowancecharge.taxCategory.ID = "S";
            allowancecharge.taxCategory.Percent = 15;
            inv.allowanceCharges.Add(allowancecharge);

           // inv.legalMonetaryTotal.PrepaidAmount = 10;
            InvoiceLine invline = new InvoiceLine();
            invline.InvoiceQuantity = 44;
            

            invline.item.Name = "Computer";
            invline.item.classifiedTaxCategory.ID = "S";
            invline.item.classifiedTaxCategory.Percent = 15;


            invline.price.PriceAmount = 25;
            invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
            invline.price.allowanceCharge.Amount = 2;
            

            TaxSubtotal taxsub = new TaxSubtotal();

            taxsub.taxCategory.ID = "S";
            taxsub.taxCategory.Percent = 15;
            taxsub.taxCategory.TaxExemptionReason = "Not subject to VAT";
            taxsub.taxCategory.TaxExemptionReason = "";
            taxsub.taxCategory.TaxExemptionReasonCode = "";
            invline.taxTotal.TaxSubtotal.Add(taxsub);
            inv.InvoiceLines.Add(invline);


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;
                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ClearanceStatus); //CLEARED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
            } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
        }

        private void btn_SimplifiedDebitNote_Click(object sender, EventArgs e)
        {
            //اشعار مدين فاتورة مبسطة
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)

            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2021-01-05";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 383;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0200000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "?Invoice Number: 354; Invoice Issue Date: 2021-02-10?";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Returned items"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى
            //بيانات البائع
            inv.SupplierParty.partyIdentification.ID = "123456";
            inv.SupplierParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "Jeddah";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.SupplierParty.partyTaxScheme.CompanyID = "310175397400003";
            // بيانات المشترى اجبارى
            inv.CustomerParty.partyIdentification.ID = "123456";
            inv.CustomerParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.CustomerParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.CustomerParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";

            //AllowanceCharge allowancecharge = new AllowanceCharge();

            //allowancecharge.Amount = 2;
            //allowancecharge.AllowanceChargeReason = "discount"; //reason
            //allowancecharge.taxCategory.ID = "S";
            //allowancecharge.taxCategory.Percent = 15;
            //inv.allowanceCharges.Add(allowancecharge);

           // inv.legalMonetaryTotal.PrepaidAmount = 10;
            InvoiceLine invline = new InvoiceLine();
            invline.InvoiceQuantity = 1;
            

            invline.item.Name = "Computer";
            invline.item.classifiedTaxCategory.ID = "S";
            invline.item.classifiedTaxCategory.Percent = 15;


            invline.price.PriceAmount = 25;
            //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
            //invline.price.allowanceCharge.Amount = 2;
            

            TaxSubtotal taxsub = new TaxSubtotal();

            taxsub.taxCategory.ID = "S";
            taxsub.taxCategory.Percent = 15;
            taxsub.taxCategory.TaxExemptionReason = "Not subject to VAT";
            taxsub.taxCategory.TaxExemptionReason = "";
            taxsub.taxCategory.TaxExemptionReasonCode = "";
            invline.taxTotal.TaxSubtotal.Add(taxsub);
            inv.InvoiceLines.Add(invline);


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;
                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ReportingStatus); //REPORTED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
            } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
        }

        private void btn_StandaredDebitNote_Click(object sender, EventArgs e)
        {
            //اشعار مدين فاتورة ضريبية
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)
            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2021-01-05";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 383;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0200000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "?Invoice Number: 354; Invoice Issue Date: 2021-02-10?";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Returned items"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى
            //بيانات البائع
            inv.SupplierParty.partyIdentification.ID = "123456";
            inv.SupplierParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "Jeddah";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.SupplierParty.partyTaxScheme.CompanyID = "310175397400003";
            // بيانات المشترى اجبارى
            inv.CustomerParty.partyIdentification.ID = "123456";
            inv.CustomerParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.CustomerParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.CustomerParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";

            //AllowanceCharge allowancecharge = new AllowanceCharge();

            //allowancecharge.Amount = 2;
            //allowancecharge.AllowanceChargeReason = "discount"; //reason
            //allowancecharge.taxCategory.ID = "S";
            //allowancecharge.taxCategory.Percent = 15;
            //inv.allowanceCharges.Add(allowancecharge);

           // inv.legalMonetaryTotal.PrepaidAmount = 10;
            InvoiceLine invline = new InvoiceLine();
            invline.InvoiceQuantity = 1;
            

            invline.item.Name = "Computer";
            invline.item.classifiedTaxCategory.ID = "S";
            invline.item.classifiedTaxCategory.Percent = 15;


            invline.price.PriceAmount = 25;
            //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
            //invline.price.allowanceCharge.Amount = 2;
            

            TaxSubtotal taxsub = new TaxSubtotal();

            taxsub.taxCategory.ID = "S";
            taxsub.taxCategory.Percent = 15;
            taxsub.taxCategory.TaxExemptionReason = "Not subject to VAT";
            taxsub.taxCategory.TaxExemptionReason = "";
            taxsub.taxCategory.TaxExemptionReasonCode = "";
            invline.taxTotal.TaxSubtotal.Add(taxsub);
            inv.InvoiceLines.Add(invline);


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;
                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ReportingStatus); //REPORTED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
            } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
        }

        private void btn_SimplifiedCreditNote_Click(object sender, EventArgs e)
        {
            //اشعار دائن فاتورة مبسطة
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)
            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2021-01-05";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 381;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0200000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "?Invoice Number: 354; Invoice Issue Date: 2021-02-10?";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Returned items"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى
            //بيانات البائع
            inv.SupplierParty.partyIdentification.ID = "123456";
            inv.SupplierParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "Jeddah";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.SupplierParty.partyTaxScheme.CompanyID = "310175397400003";
            // بيانات المشترى اجبارى
            inv.CustomerParty.partyIdentification.ID = "123456";
            inv.CustomerParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.CustomerParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.CustomerParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";

            //AllowanceCharge allowancecharge = new AllowanceCharge();

            //allowancecharge.Amount = 2;
            //allowancecharge.AllowanceChargeReason = "discount"; //reason
            //allowancecharge.taxCategory.ID = "S";
            //allowancecharge.taxCategory.Percent = 15;
            //inv.allowanceCharges.Add(allowancecharge);

           // inv.legalMonetaryTotal.PrepaidAmount = 10;
            InvoiceLine invline = new InvoiceLine();
            invline.InvoiceQuantity = 1;
            

            invline.item.Name = "Computer";
            invline.item.classifiedTaxCategory.ID = "S";
            invline.item.classifiedTaxCategory.Percent = 15;


            invline.price.PriceAmount = 25;
            //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
            //invline.price.allowanceCharge.Amount = 2;
            

            TaxSubtotal taxsub = new TaxSubtotal();

            taxsub.taxCategory.ID = "S";
            taxsub.taxCategory.Percent = 15;
            taxsub.taxCategory.TaxExemptionReason = "Not subject to VAT";
            taxsub.taxCategory.TaxExemptionReasonCode = "";
            invline.taxTotal.TaxSubtotal.Add(taxsub);
            inv.InvoiceLines.Add(invline);


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;
                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ReportingStatus); //REPORTED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
            } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
        }

        private void btn_StandardCreditNote_Click(object sender, EventArgs e)
        {
            //اشعار دائن فاتورة ضريبية
            //for information
            // VAT Category (O) "Not subject to VAT" (O)  غير خاضع لضريبة القيمة المضافة لابد ان يكون نسبة الضريبة صفر
            // VAT Category (E)   معفى من ضريبة القيمة المضافة نسبة الضريبة هتكون صفر ولابد من ذكر سبب الاعفاء :TaxExemptionReason
            // VAT Category (S)   ضريبة القيمة المضافة لابد من كتابة النسبة وتكون اكبر من صفر
            // VAT Category (Z)   Zero rated goods  البضائع الخاضعة للنسبة الصفرية 

            //PaymentMeansCode
            //10 In cash
            //30 Credit
            //42 Payment to bank account
            //48 Bank card
            //1 Instrument not defined(Free text)
            UBLXML ubl = new UBLXML();
            Invoice inv = new Invoice();
            Result res = new Result();

            inv.ID = "1230"; // مثال SME00010
            inv.IssueDate = "2021-01-05";
            inv.IssueTime = "09:32:40";
            //388 فاتورة  
            //383 اشعار مدين
            //381 اشعار دائن
            inv.invoiceTypeCode.id = 381;
            //inv.invoiceTypeCode.Name based on format NNPNESB
            //NN 01 فاتورة عادية
            //NN 02 فاتورة مبسطة
            //P فى حالة فاتورة لطرف ثالث نكتب 1 فى الحالة الاخرى نكتب 0
            //N فى حالة فاتورة اسمية نكتب 1 وفى الحالة الاخرى نكتب 0
            // E فى حالة فاتورة للصادرات نكتب 1 وفى الحالة الاخرى نكتب 0
            //S فى حالة فاتورة ملخصة نكتب 1 وفى الحالة الاخرى نكتب 0
            //B فى حالة فاتورة ذاتية نكتب 1
            //B فى حالة ان الفاتورة صادرات=1 لايمكن ان تكون الفاتورة ذاتية =1
            //
            inv.invoiceTypeCode.Name = "0100000";
            inv.DocumentCurrencyCode = "SAR";
            inv.TaxCurrencyCode = "SAR";
            // فى حالة ان اشعار دائن او مدين فقط هانكتب رقم الفاتورة اللى اصدرنا الاشعار ليها
            inv.billingReference.InvoiceDocumentReferenceID = "?Invoice Number: 354; Invoice Issue Date: 2021-02-10?";
            // قيمة عداد الفاتورة
            inv.AdditionalDocumentReferenceICV.UUID = 123456; // لابد ان يكون ارقام فقط
           //فى حالة فاتورة مبسطة وفاتورة ملخصة هانكتب تاريخ التسليم واخر تاريخ التسليم
           // inv.delivery.ActualDeliveryDate = "2022-10-22";
           // inv.delivery.LatestDeliveryDate = "2022-10-23";
           //
           //بيانات الدفع 
           // اكواد معين
           // اختيارى كود الدفع
            inv.paymentmeans.PaymentMeansCode = "42";//اختيارى
            inv.paymentmeans.InstructionNote = "Returned items"; //اجبارى فى الاشعارات
           // inv.paymentmeans.payeefinancialaccount.ID = "";//اختيارى
           // inv.paymentmeans.payeefinancialaccount.paymentnote = "Payment by credit";//اختيارى
            //بيانات البائع
            inv.SupplierParty.partyIdentification.ID = "123456";
            inv.SupplierParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.SupplierParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.SupplierParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.SupplierParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.SupplierParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.SupplierParty.postalAddress.CityName = "Jeddah";
            inv.SupplierParty.postalAddress.PostalZone = "15385";
            inv.SupplierParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.SupplierParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.SupplierParty.postalAddress.country.IdentificationCode = "SA";
            inv.SupplierParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.SupplierParty.partyTaxScheme.CompanyID = "310175397400003";
            // بيانات المشترى اجبارى
            inv.CustomerParty.partyIdentification.ID = "123456";
            inv.CustomerParty.partyIdentification.schemeID = "CRN";//رقم السجل التجارى
            inv.CustomerParty.postalAddress.StreetName = "Kemarat Street,";// اجبارى
            inv.CustomerParty.postalAddress.AdditionalStreetName = ""; //اختيارى
            inv.CustomerParty.postalAddress.BuildingNumber = "3724"; // اجبارى
            inv.CustomerParty.postalAddress.PlotIdentification = "9833";//اختيارى
            inv.CustomerParty.postalAddress.CityName = "Jeddah";
            inv.CustomerParty.postalAddress.PostalZone = "15385";
            inv.CustomerParty.postalAddress.CountrySubentity = "Makkah";// اختيارى
            inv.CustomerParty.postalAddress.CitySubdivisionName = "Alfalah";
            inv.CustomerParty.postalAddress.country.IdentificationCode = "SA";
            inv.CustomerParty.partyLegalEntity.RegistrationName = "First Shop";
            inv.CustomerParty.partyTaxScheme.CompanyID = "301121971100003";

            //AllowanceCharge allowancecharge = new AllowanceCharge();

            //allowancecharge.Amount = 2;
            //allowancecharge.AllowanceChargeReason = "discount"; //reason
            //allowancecharge.taxCategory.ID = "S";
            //allowancecharge.taxCategory.Percent = 15;
            //inv.allowanceCharges.Add(allowancecharge);

           // inv.legalMonetaryTotal.PrepaidAmount = 10;
            InvoiceLine invline = new InvoiceLine();
            invline.InvoiceQuantity = 1;
            

            invline.item.Name = "Computer";
            invline.item.classifiedTaxCategory.ID = "S";
            invline.item.classifiedTaxCategory.Percent = 15;


            invline.price.PriceAmount = 25;
            //invline.price.allowanceCharge.AllowanceChargeReason = "discount"; //reason
            //invline.price.allowanceCharge.Amount = 2;
            

            TaxSubtotal taxsub = new TaxSubtotal();

            taxsub.taxCategory.ID = "S";
            taxsub.taxCategory.Percent = 15;
            taxsub.taxCategory.TaxExemptionReason = "Not subject to VAT";
            taxsub.taxCategory.TaxExemptionReason = "";
            taxsub.taxCategory.TaxExemptionReasonCode = "";
            invline.taxTotal.TaxSubtotal.Add(taxsub);
            inv.InvoiceLines.Add(invline);


            res = ubl.GenerateInvoiceXML(inv);
            if (res.IsValid)
            {
                //MessageBox.Show(res.InvoiceHash);
                //MessageBox.Show(res.SingedXML);
                //MessageBox.Show(res.EncodedInvoice);
                //MessageBox.Show(res.UUID);
                //MessageBox.Show(res.QRCode);
                //MessageBox.Show(res.PIH);
                //MessageBox.Show(res.SingedXMLFileName);
            }
            else
            {
                MessageBox.Show(res.ErrorMessage);
                return;
            }


           ApiRequestLogic apireqlogic = new ApiRequestLogic();
           ComplianceCsrResponse tokenresponse = new ComplianceCsrResponse();

            InvoiceReportingRequest invrequestbody = new InvoiceReportingRequest();
            tokenresponse = apireqlogic.GetComplianceCSIDAPI("12345", "");
            if (string.IsNullOrEmpty(tokenresponse.ErrorMessage))
            {
                //MessageBox.Show(tokenresponse.BinarySecurityToken);
                invrequestbody.invoice = res.EncodedInvoice;
                invrequestbody.invoiceHash = res.InvoiceHash;
                invrequestbody.uuid = res.UUID;
                InvoiceReportingResponse invoicereportingmodel = apireqlogic.CallComplianceInvoiceAPI(tokenresponse.BinarySecurityToken, tokenresponse.Secret, invrequestbody);
                if (string.IsNullOrEmpty(invoicereportingmodel.ErrorMessage))
                {
                    MessageBox.Show(invoicereportingmodel.ClearanceStatus); //REPORTED
                }
                else
                {
                    MessageBox.Show(invoicereportingmodel.ErrorMessage);
                }
            } else
            {
                MessageBox.Show(tokenresponse.ErrorMessage);
            }
        }
    }
}
