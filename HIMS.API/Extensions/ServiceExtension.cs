﻿using HIMS.Data;
using HIMS.Data.IPD;
using HIMS.Data.Master;
using HIMS.Data.Master.Billing;
using HIMS.Data.Master.DepartMentMaster;
using HIMS.Data.Master.DoctorMaster;
using HIMS.Data.Master.PersonalDetails;
using HIMS.Data.Master.Inventory;
using HIMS.Data.Master.Radiology;
using HIMS.Data.Master.Pathology;
using HIMS.Data.Master.Prescription;
using HIMS.Data.Master.VendorMaster;
using HIMS.Data.Opd;
using HIMS.Data.Pathology;
using HIMS.Data.Radiology;
using HIMS.Data.Pharmacy;
using HIMS.Model;
using HIMS.Model.Radiology;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using HIMS.Data.Opd.OP;
using HIMS.API.Utility;
using Microsoft.AspNetCore.Http.Features;
using HIMS.Data.HomeTransaction;
using HIMS.Data.Inventory;
using HIMS.Data.Users;
using HIMS.Data.Dashboard;
using HIMS.Data.WhatsAppEmail;
using HIMS.Data.HomeDelivery;
using HIMS.Data.CustomerInformation;
using HIMS.Data.CustomerPayment;
using HIMS.Data.OPReports;
using HIMS.Data.CommanReports;
using HIMS.Data.MISReports;
using HIMS.Data.GSTReports;
using HIMS.Data.InventoryReports;
using HIMS.Data.DoctorShareReports;
using HIMS.Data.PharmacyReports;
using HIMS.Data.Administration;
using HIMS.Data.Document;
using HIMS.Data.CustomerAMCInfo;
using HIMS.Data.OT;
using HIMS.Data.Nursing;
namespace HIMS.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddLinuxApacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //configuration for linux apache
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse(configuration.GetValue<string>("AppSettings:IPAddress")));
            });
        }

        public static void AddMyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    );
            });
        }

        public static void AddMyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue;
                o.MultipartHeadersLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
                options.AllowSynchronousIO = true;
            });

            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);
        }

        public static void AddMyDependancies(this IServiceCollection services, IConfiguration configuration)
        {
            //var conn = configuration.GetValue<string>("AppSettings:ConnectionString");
            //Get Env from EnvVariable
            var conn = configuration.GetValue<string>("AppSettings:CONNECTION_STRING");
            services.AddScoped(sqc => new SqlConnection(conn));
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddTransient<I_OpdBrowseList, R_OpdBrowseList>();
            services.AddTransient<I_CashCounterMaster, R_CashCounterMaster>();
            services.AddTransient<I_OpdAppointment, R_OpdAppointment>();
            services.AddTransient<IGenericComboRepository, GenericComboRepository>();
            services.AddTransient<IComboboxRepository, ComboboxRepository>();
            services.AddTransient<IBrowseBillRepository, BrowseBillRepository>();
            services.AddTransient<I_LoginManager, R_LoginManager>();
            services.AddTransient<I_BankMaster, R_BankMaster>();
            services.AddTransient<I_SubGroupMaster, R_SubGroupMaster>();
            // services.AddTransient<I_BillingClassMaster, R_BillingClassMaster>();
            services.AddTransient<I_CountryMaster, R_CountryMaster>();
            services.AddTransient<I_OpdAppointmentList, R_OpdAppointmentList>();
            services.AddTransient<I_PathologyPatientList, R_PathologyPatientList>();
            services.AddTransient<I_ServiceMaster, R_ServiceMaster>();
            services.AddTransient<I_DoctorMaster, R_DoctorMaster>();
            services.AddTransient<I_Addcharges, R_AddCharges>();
            services.AddTransient<I_ComAddcharges, R_ComAddCharges>();
            services.AddTransient<I_GenderMaster, R_GenderMaster>();
            services.AddTransient<I_PrefixMaster, R_PrefixMaster>();
            services.AddTransient<I_PatientTypeMaster, R_PatientTypeMaster>();
            services.AddTransient<I_MaritalStatusMaster, R_MaritalStatusMaster>();
            services.AddTransient<I_RelationshipMaster, R_RelationshipMaster>();
            services.AddTransient<I_ReligionMaster, R_ReligionMaster>();
            services.AddTransient<I_CountryMaster, R_CountryMaster>();
            services.AddTransient<I_StateMaster, R_StateMaster>();
            services.AddTransient<I_CityMaster, R_CityMaster>();
            services.AddTransient<I_TalukaMaster, R_TalukaMaster>();
            services.AddTransient<I_VillageMaster, R_VillageMaster>();
            services.AddTransient<I_AreaMaster, R_AreaMaster>();
            services.AddTransient<I_CurrencyMaster, R_CurrencyMaster>();
            services.AddTransient<I_ItemCategoryMaster, R_ItemCategoryMaster>();
            services.AddTransient<I_DepartmentMaster, R_DepartmentMaster>();
            services.AddTransient<I_LocationMaster, R_LocationMaster>();
            services.AddTransient<I_WardMaster, R_WardMaster>();
            services.AddTransient<I_BedMaster, R_BedMaster>();
            services.AddTransient<I_DischargeTypeMaster, R_DischargeTypeMaster>();
            services.AddTransient<I_ItemClassMaster, R_ItemClassMaster>();
            services.AddTransient<I_ItemGenericMaster, R_ItemGenericMaster>();
            services.AddTransient<I_ItemTypeMaster, R_ItemTypeMaster>();
            services.AddTransient<I_ManufactureMaster, R_ManufactureMaster>();
            services.AddTransient<I_UnitofMeasurementMaster, R_UnitofMeasurement>();
            services.AddTransient<I_TaxMaster, R_TaxMaster>();
            services.AddTransient<I_ModeofPaymentMaster, R_ModeofPaymentMaster>();
            services.AddTransient<I_TermsofPaymentMaster, R_TermsofPaymentMaster>();
            services.AddTransient<I_CategoryMaster, R_CategoryMaster>();
            services.AddTransient<I_RadiologyTemplateMaster, R_RadiologyTemplateMaster>();
            services.AddTransient<I_ParameterMasterAgeWise, R_ParameterMasterAgeWise>();
            services.AddTransient<I_PathologyCategoryMaster, R_PathologyCategoryMaster>();
            services.AddTransient<I_UnitMaster, R_UnitMaster>();
            services.AddTransient<I_GenericMaster, R_GenericMaster>();
            services.AddTransient<I_DoseMaster, R_DoseMaster>();
            services.AddTransient<I_DrugMaster, R_DrugMaster>();
            services.AddTransient<I_StoreMaster, R_StoreMaster>();
            services.AddTransient<I_InstructionMaster, R_InstructionMaster>();
            services.AddTransient<I_PathologyTestMaster, R_PathologyTestMaster>();
            services.AddTransient<I_RadiologyTestMaster, R_RadiologyTestMaster>();
            services.AddTransient<I_PathParameterMaster, R_PathParameterMaster>();
            services.AddTransient<I_PhoneAppointment, R_PhoneAppoinment>();
            services.AddTransient<I_Payment, R_Payment>();
            // services.AddTransient<I_SaveAppointmentNewPatient, R_SaveAppointmentNewPatient>();
            services.AddTransient<I_PrescriptionTemplateMaster, R_PrescriptionTemplateMaster>();
            services.AddTransient<I_MenuMaster, R_MenuMaster>();
             services.AddTransient<I_PayTranModeMaster, R_PayTranModeMaster>();
            //services.AddTransient<I_ProductTypeMasterHome, R_ProductTypeMaster>();
            services.AddTransient<I_MenuMasterDetails, R_MenuMasterDetails>();
            services.AddTransient<I_MenuMasterDetails_Details, R_MenuMasterDetails_Details>();
            services.AddTransient<I_VendorMaster, R_VendorMaster>();
            services.AddTransient<I_GroupMaster, R_GroupMaster>();
            services.AddTransient<I_ClassMaster, R_ClassMaster>();
            services.AddTransient<I_CompanyMaster, R_CompanyMaster>();
            services.AddTransient<I_ConsessionReasonMaster, R_ConsessionReasonMaster>();
            services.AddTransient<I_CompanyTypeMaster, R_CompanyTypeMaster>();
            //services.AddTransient<I_ItemMaster, R_ItemMaster>();
            services.AddTransient<I_ParameterMasterAgeWise, R_ParameterMasterAgeWise>();
            services.AddTransient<I_SupplierMaster, R_SupplierMaster>();
            //services.AddTransient<I_StoreMaster, R_StoreMaster>();
            services.AddTransient<I_Administration, R_Administration>();
            services.AddTransient<I_Constants, R_Constants>();

            services.AddTransient<I_IssueToDepartmentIndent, R_IssueToDepartmentIndent>();
            services.AddTransient<I_DoctorTypeMaster, R_DoctorTypeMaster>();
            services.AddTransient<I_AdmissionReg, R_AdmissionReg>();
            services.AddTransient<I_RegisteredPatientAdmission, R_RegisteredPatientAdmission>();
            services.AddTransient<I_OpdCasePaper, R_OpdCasePaper>();
            services.AddTransient<I_CompanyTypeMaster, R_CompanyTypeMaster>();
            services.AddTransient<I_PathologyTemplateMaster, R_PathologyTemplateMaster>();
            services.AddTransient<I_PathologyTestMaster, R_PathologyTestMaster>();
            services.AddTransient<I_OpdBrowseList, R_OpdBrowseList>();
            services.AddTransient<I_Payment, R_Payment>();
            services.AddTransient<I_PhoneAppointment, R_PhoneAppoinment>();
            services.AddTransient<I_OPDRegistration, R_OPDRegistration>();
            services.AddTransient<I_IPDischarge, R_IPDischarge>();
            services.AddTransient<I_IPDDischargeSummary, R_IPDDischargeSummary>();
            services.AddTransient<I_IPRefundofAdvance, R_IPRefundofAdvance>();
            services.AddTransient<I_IPRefundofBilll, R_IPRefundofBilll>();
            // services.AddTransient<I_TariffMaster,R_TariffMaster>();
            services.AddTransient<I_SubTpaCompanyMaster, R_SubTpaCompanyMaster>();

            services.AddTransient<I_IPDEmergency, R_IPDEmergency>();
            services.AddTransient<I_MLCInfo, R_MLCInfo>();
            services.AddTransient<I_IPPrescriptionReturn, R_IPPrescriptionReturn>();
            services.AddTransient<I_pathologyReportDetail, R_pathologyReportDetail>();
            services.AddTransient<I_PathologySampleCollection, R_PathologySampleCollection>();
            services.AddTransient<I_OPDPrescription, R_OPDPrescription>();
            services.AddTransient<I_IPAdvance, R_IPAdvance>();
            services.AddTransient<I_IPAdvanceUpdate, R_IPAdvanceUpdate>();
            services.AddTransient<I_IPBilling, R_IPBilling>();
            services.AddTransient<I_IPInterimBill, R_IPInterimBill>();
            services.AddTransient<I_LAD_RAD, R_LAD_RAD>();
            services.AddTransient<I_IPPrescription, R_IPPrescription>();
            services.AddTransient<I_IP_Charges_Delete, R_IP_Charges_Delete>();
            services.AddTransient<I_RadiologyTemplateResult, R_RadiologyTemplateResult>();
            services.AddTransient<I_PathologyTemplateResult, R_PathologyTemplateResult>();
            services.AddTransient<I_OPRefundBill, R_OPRefundBill>();
            services.AddTransient<I_BedTransfer, R_BedTransfer>();
            services.AddTransient<I_RegisteredPatientAdmission, R_RegisteredPatientAdmission>();
            services.AddTransient<I_SS_RoleTemplateMaster, R_SS_RoleTemplateMaster>();
            services.AddTransient<I_OPbilling, R_OpBilling>();
            services.AddTransient<I_CasePaperPrescription, R_CasePaperPrescription>();
            services.AddTransient<I_OPAddCharges, R_OPAddCharges>();
            services.AddTransient<I_Admission, R_Admission>();
            services.AddTransient<I_RegisteredPatientAdmission, R_RegisteredPatientAdmission>();
            services.AddTransient<I_Pathologysamplecollection, R_Pathologysamplecollection>();
            services.AddTransient<I_pathresultentry, R_pathresultentry>();
            services.AddTransient<I_IPLabrequestChange, R_IPLabrequestChange>();
            services.AddTransient<I_DynamicExecuteSchedule, R_DynamicExecuteSchedule>();
            services.AddTransient<I_Emailconfiguration, R_Emailconfiguration>();
            services.AddTransient<I_Configsetting, R_Configsetting>();


            services.AddTransient<I_OpdBrowseList, R_OpdBrowseList>();
            //services.AddTransient<ICashCounterRepository, CashCounterRepository>();
            services.AddTransient<I_OpdAppointment, R_OpdAppointment>();
            services.AddTransient<IGenericComboRepository, GenericComboRepository>();
            services.AddTransient<IComboboxRepository, ComboboxRepository>();
            services.AddTransient<IBrowseBillRepository, BrowseBillRepository>();
            services.AddTransient<I_LoginManager, R_LoginManager>();
            //  services.AddTransient<I_BankMaster, R_BankMaster>();
            //services.AddTransient<I_BillingClassMaster, R_BillingClassMaster>();
            services.AddTransient<I_CountryMaster, R_CountryMaster>();
            services.AddTransient<I_GenderMaster, R_GenderMaster>();
            services.AddTransient<I_OpdAppointmentList, R_OpdAppointmentList>();
            services.AddTransient<I_PathologyPatientList, R_PathologyPatientList>();
            services.AddTransient<I_ServiceMaster, R_ServiceMaster>();
            services.AddTransient<I_VendorMaster, R_VendorMaster>();
            //services.AddTransient<I_ProductTypeMasterHome, R_ProductTypeMaster>();

            services.AddTransient<I_MenuMaster, R_MenuMaster>();
            services.AddTransient<I_MenuMasterDetails, R_MenuMasterDetails>();
            services.AddTransient<I_MenuMasterDetails_Details, R_MenuMasterDetails_Details>();
            
            services.AddTransient<I_CanteenRequest,R_CanteenRequest>();

            services.AddTransient<I_InsertIPDraft, R_InsertIPDraft>();
            services.AddTransient<I_IPPathOrRadiRequest, R_IPPathOrRadiRequest>();
            services.AddTransient<I_EmailNotification, R_EmailNotification>();

            services.AddTransient<I_IPBillingwithcredit, R_IPBillingwithcredit>();
            services.AddTransient<I_OPBillingCredit, R_OPBillingCredit>();
            services.AddTransient<I_Payment, R_Payment>();
            services.AddTransient<I_IPBillEdit, R_IPBillEdit>();
            services.AddTransient<I_IPAdvanceEdit, R_IPAdvanceEdit>();
            services.AddTransient<I_OPSettlemtCredit, R_OPSettlemtCredit>();
            services.AddTransient<I_IP_Settlement_Process, R_IP_Settlement_Process>();
            services.AddTransient<I_DocumentAttachment, R_DocumentAttachment>();
            services.AddTransient<I_IP_SMSOutgoing, R_IP_SMSOutgoing>();
            services.AddTransient<I_OpeningTransaction, R_OpeningTransaction>();



            services.AddTransient<I_OTTableDetail, R_OTTableDetail>();
            services.AddTransient<I_OTBookingDetail, R_OTBookingDetail>();
            services.AddTransient<I_CathLabBookingDetail, R_CathLabBookingDetail>();
            services.AddTransient<I_IPPrescription, R_IPPrescription>();
            services.AddTransient<I_OTEndoscopy, R_OTEndoscopy>();
            services.AddTransient<I_OTRequest, R_OTRequest>();

            services.AddTransient<I_OTNotesTemplate, R_OTNotesTemplate>();
            services.AddTransient<I_MaterialConsumption, R_MaterialConsumption>();
            services.AddTransient<I_NeroSurgeryOTNotes, R_NeroSurgeryOTNotes>();
            services.AddTransient<I_DoctorNote, R_DoctorNote>();
            services.AddTransient<I_NursingTemplate, R_NursingTemplate>();
            services.AddTransient<I_Mrdmedicalcertificate, R_Mrdmedicalcertificate>();
            services.AddTransient<I_Mrddeathcertificate, R_Mrddeathcertificate>();
            services.AddTransient<I_SubcompanyTPA, R_SubcompanyTPA>();
            services.AddTransient<I_Prepostopnote, R_Prepostopnote>();
            services.AddTransient<I_PatientFeedback, R_PatientFeedback>();
            services.AddTransient<I_SMS_Config,R_SMSConfig>();
            services.AddTransient<I_InvOpeningBalance, R_InvOpeningBalance>();


            services.AddTransient<I_ItemMaster, R_ItemMaster>();
            services.AddTransient<I_Sales, R_Sales>();
            services.AddTransient<I_SalesReturn, R_SalesReturn>();
            services.AddTransient<I_PurchaseOrder, R_PurchaseOrder>();
            services.AddTransient<I_GRN, R_GRN>();
            services.AddTransient<I_MaterialAcceptance, R_MaterialAcceptance>();

            services.AddTransient<IPdfUtility, PdfUtility>();
            services.AddTransient<IFileUtility, FileUtility>();
            services.AddTransient<I_Workorder, R_Workorder>();
            services.AddTransient<I_WhatsappSms, R_WhatsappSms>();
            services.AddTransient<I_PatientDocumentupload, R_PatientDocumentupload>();
            services.AddTransient<I_IssueTrackingInfo, R_IssueTrackingInfo>();
            services.AddTransient<I_Indent, R_Indent>();
            services.AddTransient<I_IssuetoDepartment, R_IssuetoDepartment>();

            services.AddTransient<I_Role, R_Role>();
            services.AddTransient<I_Dashboard, R_Dashboard>();
            services.AddTransient<I_Favourite, R_Favourite>();

            services.AddTransient<I_Stokadjustment, R_Stockadjustment>();
            services.AddTransient<I_Mrpadjustment, R_Mrpadjustment>();
            services.AddTransient<I_Openingbalance, R_OpeningBalance>();
            services.AddTransient<I_DoctorShare, R_DoctorShare>();
            services.AddTransient<I_CrossConsultation, R_CrossConsultation>();

            services.AddTransient<I_UserChangePassword, R_UserChangePassword>();
            services.AddTransient<I_PharmPaymentMode, R_PharmPaymentMode>();
            services.AddTransient<I_InvMaterialConsumption, R_InvMaterialConsumption>();
            services.AddTransient<I_GRNReturn, R_GRNReturn>();
            services.AddTransient<I_ReturnFromDept, R_ReturnFromDept>();

            services.AddTransient<I_HomeDeliveryLogin, R_HomeDeliveryLogin>();
            services.AddTransient<I_HomeDeliveryOrder, R_HomeDeliveryOrder>();
            services.AddTransient<I_SalesReport, R_SalesReport>();

            services.AddTransient<I_IPReports, R_IPReports>();
            services.AddTransient<I_OPBillingReport, R_OPBillingReport>();

            services.AddTransient<I_StockAdjustment, R_StockAdjustment>();
            
            services.AddTransient<I_CustomerInformation, R_CustomerInformation>();
            services.AddTransient<I_CustomerPayments, R_CustomerPayment>();
            services.AddTransient<I_CustomerInvoiceRaise, R_CustomerInvoiceRaise>();
            services.AddTransient<I_CustomerAMCInfo, R_CustomerAMCInfo>();

            services.AddTransient<I_ScheduleMaster, R_ScheduleMaster>();
            services.AddTransient<I_Itemmovement, R_Itemmovement>();
            services.AddTransient<I_Hospital, R_Hospital>();
            services.AddTransient<I_CommanReport, R_CommanReport>();
            services.AddTransient<I_MISReport, R_MISReport>();
            services.AddTransient<I_GSTReport, R_GSTReport>();
            services.AddTransient<I_InventoryReport, R_InventoryReport>();
            services.AddTransient<I_DoctorShareReport, R_DoctorShareReport>();
            services.AddTransient<I_PharmacyReports, R_PharmacyReports>();
            services.AddTransient<I_PHAdvance, R_PHAdvance>();
            services.AddTransient<I_PHAdvanceRefund, R_PHAdvanceRefund>();
            services.AddTransient<I_CompanyInformation, R_CompanyInformation>();
           services.AddTransient<I_SupplierPayment, R_SupplierPayment>();
            services.AddTransient<I_Document, R_Document>();
            services.AddTransient<I_HelthCard, R_HealthCard>();

            services.AddTransient<I_OT, R_OT>();
            services.AddTransient<I_Nursing, R_Nursing>();



            services.AddTransient<I_PrescriptionTemplate, PrescriptionTemplate>();

           services.AddTransient<I_NewTemplateDescription, R_NewTemplateDescription>();
            services.AddTransient<I_NewMenuMaster, R_NewMenuMaster>();

            services.AddTransient<I_Report, R_Report>();
            /* services.AddTransient<I_CountryMasterHome, R_CountryMasterHome>();
            services.AddTransient<I_GenderMasterHome, R_GenderMasterHome>();
            services.AddTransient<I_BillingClassMasterHome, R_BillingClassMasterHome>();
            services.AddTransient<I_Itemmasterhome, R_Itemmasterhome>();

           services.AddTransient<I_ItemCategoryMasterHome, R_ItemCategoryMasterHome>();
           services.AddTransient<I_MenuMasterHome, R_MenuMasterHome>();
           services.AddTransient<I_MenuMasterDetailsHome, R_MenuMasterDetailsHome>();
           services.AddTransient<I_MenuMasterDetails_DetailsHome, R_MenuMasterDetails_DetailsHome>();*/

            // services.AddTransient<>();

        }

        public static void AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings objects
            //var appSettingsSection = configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            var secret = configuration.GetValue<string>("AppSettings:SECRET");
            //var secret = Environment.GetEnvironmentVariable("SECRET");
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();
        }

        public static void AddMySwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HIMS AIRMID API",
                    Description = "Hospital Informaion Management System"
                });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
            });
        }

    }
}
