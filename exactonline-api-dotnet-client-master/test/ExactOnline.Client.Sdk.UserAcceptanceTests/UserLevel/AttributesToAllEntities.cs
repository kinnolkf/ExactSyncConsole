using ExactOnline.Client.Models;
using ExactOnline.Client.Sdk.Controllers;
using ExactOnline.Client.Sdk.TestContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExactOnline.Client.Sdk.UserAcceptanceTests.UserLevel
{
	[TestClass]
	public class AttributesToAllEntities
	{
		[TestMethod]
		[TestCategory("User Acceptance Tests")]
		public void ReadAllEntities()
		{
			var toc = new TestObjectsCreator();
			var client = new ExactOnlineClient(toc.EndPoint, toc.GetOAuthAuthenticationToken);

			// Not supported entities
			//var printedSalesInvoiceCollection = client.For<PrintedSalesInvoice>().Get(); // Does only support post		
			//var salesItemPriceCollection = client.For<SalesItemPrice>().Get(); // Is a function
			//var stockPositionCollection = client.For<StockPosition>().Get(); // Is a function

			// Read Entities
			var agingOverviewCollection = client.For<AgingOverview>().Select("AmountPayable").Get();
			var accountCollection = client.For<Account>().Select("ID").Get();
			var accountClassCollection = client.For<AccountClass>().Select("ID").Get();
			var assetCollection = client.For<Asset>().Select("ID").Get();
			var bankAccountCollection = client.For<BankAccount>().Select("ID").Get();
			var budgetCollection = client.For<Budget>().Select("ID").Get();
			var contactCollection = client.For<Contact>().Select("ID").Get();
			var costcenterCollection = client.For<Costcenter>().Select("ID").Get();
			var costTransactionCollection = client.For<CostTransaction>().Select("ID").Get();
			var costunitCollection = client.For<Costunit>().Select("ID").Get();
			var divisionCollection = client.For<Division>().Select("HID").Get();
			var documentCollection = client.For<Document>().Select("ID").Get();
			var documentAttachmentCollection = client.For<DocumentAttachment>().Select("ID").Get();
			var documentCategoryCollection = client.For<DocumentCategory>().Select("ID").Get();
			var documentTypeCollection = client.For<DocumentType>().Select("ID").Get();
			var documentTypeCategoryCollection = client.For<DocumentTypeCategory>().Select("ID").Get();
			var employeeCollection = client.For<Employee>().Select("ID").Get();
			var exchangeRateCollection = client.For<ExchangeRate>().Select("ID").Get();
			var financialPeriodCollection = client.For<FinancialPeriod>().Select("ID").Get();
			var glAccountCollection = client.For<GLAccount>().Select("ID").Get();
			var glClassificationCollection = client.For<GLClassification>().Select("ID").Get();
			var glSchemeCollection = client.For<GLScheme>().Select("ID").Get();
			var itemCollection = client.For<Item>().Select("ID").Get();
			var itemGroupCollection = client.For<ItemGroup>().Select("ID").Get();
			var journalCollection = client.For<Journal>().Select("ID").Get();						
			var layoutCollection = client.For<Layout>().Select("ID").Get();
			var mailboxCollection = client.For<Mailbox>().Select("ID").Get();
			var mailMessageCollection = client.For<MailMessage>().Select("ID").Get();
			var mailMessageAttachmentCollection = client.For<MailMessageAttachment>().Select("ID").Get();
			var operationCollection = client.For<Operation>().Select("ID").Get();
			var operationResourceCollection = client.For<OperationResource>().Select("ID").Get();
			var opportunityCollection = client.For<Opportunity>().Select("ID").Get();
			var opportunityContactCollection = client.For<OpportunityContact>().Select("ID").Get();
			var outstandingInvoiceOverviewCollection = client.For<OutstandingInvoiceOverview>().Select("CurrencyCode").Get();
			var paymentConditionCollection = client.For<PaymentCondition>().Select("ID").Get();
			//var periodRevenueCollection = client.For<PeriodRevenue>().Select("Year,Period").Get(); compound key not supported
			var priceListCollection = client.For<PriceList>().Select("ID").Get();
			var productionAreaCollection = client.For<ProductionArea>().Select("ID").Get();
			var salesEntryCollection = client.For<SalesEntry>().Select("EntryID").Get();
			var salesEntryLineCollection = client.For<SalesEntryLine>().Select("ID").Get();
			var salesInvoiceCollection = client.For<SalesInvoice>().Select("InvoiceID").Get();
			var salesInvoiceLineCollection = client.For<SalesInvoiceLine>().Select("ID").Get();
			var shopOrderCollection = client.For<ShopOrder>().Select("ID").Get();
			var shopOrderMaterialPlanCollection = client.For<ShopOrderMaterialPlan>().Select("ID").Get();
			var shopOrderRoutingStepPlanCollection = client.For<ShopOrderRoutingStepPlan>().Select("ID").Get();
			var taxDocumentCollection = client.For<TaxDocument>().Select("DocumentID").Get();
			var timeTransactionCollection = client.For<TimeTransaction>().Select("ID").Get();
			var unitCollection = client.For<Unit>().Select("ID").Get();
			var userCollection = client.For<User>().Select("UserID").Get();
			var userRoleCollection = client.For<UserRole>().Select("ID").Get();
			var vatCodeCollection = client.For<VATCode>().Select("Code").Get();
			var warehouseCollection = client.For<Warehouse>().Select("ID").Get();
			var workcenterCollection = client.For<Workcenter>().Select("ID").Get();
		}
	}
}
