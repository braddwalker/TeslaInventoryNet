using System;
using Newtonsoft.Json;

namespace TeslaInventoryNet
{
    public class CompositorViews
    {
        [JsonProperty("frontView")]
        public string FrontView { get; set; }
        
        [JsonProperty("sideView")]
        public string SideView { get; set; }
        
        [JsonProperty("interiorView")]
        public string InteriorView { get; set; }
    }

    /// <summary>
    /// These are not part of the underlying data model, but rather they get computed
    /// once search results get returned from the API.
    /// </summary>
    public class CompositorUrls
    {
        [JsonProperty("frontView")]
        public string FrontView { get; set; }
        
        [JsonProperty("sideView")]
        public string SideView { get; set; }
        
        [JsonProperty("interiorView")]
        public string InteriorView { get; set; }
    }

    public class FlexibleOptionsData
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("group")]
        public string Group { get; set; }
        
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("price")]
        public int Price { get; set; }
    }

    public class LexiconDefaultOption
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("group")]
        public string Group { get; set; }
        
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class DownPayment
    {
        [JsonProperty("downPaymentAmount")]
        public int DownPaymentAmount { get; set; }

        [JsonProperty("downPaymentPercent")]
        public double DownPaymentPercent { get; set; }

        [JsonProperty("normalizedMaximumDownPayment")]
        public int NormalizedMaximumDownPayment { get; set; }

        [JsonProperty("normalizedMinimumDownPayment")]
        public int NormalizedMinimumDownPayment { get; set; }
    }

    public class Loan
    {
        [JsonProperty("DNBAdminFee")]
        public int DNBAdminFee { get; set; }

        [JsonProperty("DNBLoanFee")]
        public int DNBLoanFee { get; set; }

        [JsonProperty("amountDueAtSigning")]
        public int AmountDueAtSigning { get; set; }

        [JsonProperty("balloonPayment")]
        public int BalloonPayment { get; set; }

        [JsonProperty("balloonPaymentPercent")]
        public double BalloonPaymentPercent { get; set; }

        [JsonProperty("costOfCredit")]
        public int? CostOfCredit { get; set; }

        [JsonProperty("downPayment")]
        public DownPayment DownPayment { get; set; }

        [JsonProperty("effectiveRate")]
        public double EffectiveRate { get; set; }

        [JsonProperty("effectiveRateAverage")]
        public string EffectiveRateAverage { get; set; }

        [JsonProperty("financedAmount")]
        public int FinancedAmount { get; set; }

        [JsonProperty("financedAmountInclFees")]
        public int FinancedAmountInclFees { get; set; }

        [JsonProperty("insuranceAmt")]
        public int InsuranceAmt { get; set; }

        [JsonProperty("interestRate")]
        public double InterestRate { get; set; }

        [JsonProperty("maxballoonPaymentAmount")]
        public int MaxballoonPaymentAmount { get; set; }

        [JsonProperty("maximumBalloonPayment")]
        public int MaximumBalloonPayment { get; set; }

        [JsonProperty("minimumBalloonPayment")]
        public int MinimumBalloonPayment { get; set; }

        [JsonProperty("monthlyPayment")]
        public int MonthlyPayment { get; set; }

        [JsonProperty("netMonthlyPayment")]
        public int? NetMonthlyPayment { get; set; }

        [JsonProperty("nominalRate")]
        public double NominalRate { get; set; }

        [JsonProperty("regional")]
        public string[] Regional { get; set; }

        [JsonProperty("residualAmount")]
        public int ResidualAmount { get; set; }

        [JsonProperty("residualValueRate")]
        public int ResidualValueRate { get; set; }

        [JsonProperty("term")]
        public int Term { get; set; }
    }

    public class LoanDetails
    {
        [JsonProperty("loan")]
        public Loan Loan { get; set; }
    }

    public class OptionCodeData
    {
        [JsonProperty("acceleration_unit_long")]
        public string AccelerationUnitLong { get; set; }

        [JsonProperty("acceleration_unit_short")]
        public string AccelerationUnitShort { get; set; }

        [JsonProperty("acceleration_value")]
        public string AccelerationValue { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("unit_long")]
        public string UnitLong { get; set; }

        [JsonProperty("unit_short")]
        public string UnitShort { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("range_source")]
        public string RangeSource { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class OptionCodePricing
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }

    public class OrderFee
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Query
    {
        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }
    }

    public class Fee
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class MetroFee
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("destination_metro")]
        public string DestinationMetro { get; set; }

        [JsonProperty("source_metro")]
        public string SourceMetro { get; set; }
    }

    public class TransportFees
    {
        [JsonProperty("exemptVRL")]
        public int[] ExemptVRL { get; set; }

        [JsonProperty("fees")]
        public Fee[] Fees { get; set; }

        [JsonProperty("metro_fees")]
        public MetroFee[] MetroFees { get; set; }
    }

    public class WarrantyData
    {
        public int UsedVehicleLimitedWarrantyMile { get; set; }
        public int UsedVehicleLimitedWarrantyYear { get; set; }
        public DateTime? WarrantyBatteryExpDate { get; set; }
        public bool WarrantyBatteryIsExpired { get; set; }
        public int WarrantyBatteryMile { get; set; }
        public int WarrantyBatteryYear { get; set; }
        public DateTime? WarrantyDriveUnitExpDate { get; set; }
        public int WarrantyDriveUnitMile { get; set; }
        public int WarrantyDriveUnitYear { get; set; }
        public int WarrantyMile { get; set; }
        public DateTime? WarrantyVehicleExpDate { get; set; }
        public bool WarrantyVehicleIsExpired { get; set; }
        public int WarrantyYear { get; set; }
    }

    public class Option
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("list")]
        public string[] List { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }
    }

    public class OptionCodes
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("options")]
        public Option[] Options { get; set; }
    }

    public class OptionCodeSpecs
    {
        [JsonProperty("C_SPECS")]
        public OptionCodes CSpecs { get; set; }

        [JsonProperty("C_DESIGN")]
        public OptionCodes CDesign { get; set; }

        [JsonProperty("C_CALLOUTS")]
        public OptionCodes CCallouts { get; set; }

        [JsonProperty("C_OPTS")]
        public OptionCodes COptions { get; set; }
    }

    public class ExternalZoom
    {
        [JsonProperty("order")]
        public double Order { get; set; }
        
        [JsonProperty("details")]
        public int Details { get; set; }

        [JsonProperty("search")]
        public double Search { get; set; }
    }

    public class CompositorViewsCustom
    {
        [JsonProperty("isProductWithCustomViews")]
        public bool IsProductWithCustomViews { get; set; }

        [JsonProperty("externalZoom")]
        public ExternalZoom ExternalZoom { get; set; }
    }

    public class Vehicle
    {
        public bool InTransit { get; set; }

        [JsonProperty("ADL_OPTS")]
        public string[] AdditionalOptions { get; set; }

        [JsonProperty("AUTOPILOT")]
        public string[] Autopilot { get; set; }
        public DateTime? ActualGAInDate { get; set; }
        public string AddToInventoryReason { get; set; }

        [JsonProperty("BATTERY")]
        public string[] Battery { get; set; }

        [JsonProperty("CABIN_CONFIG")]
        public string[] CabinConfig { get; set; }
        public string CPORefurbishmentStatus { get; set; }
        public string CPOWarrantyType { get; set; }
        public string City { get; set; }
        public CompositorViews CompositorViews { get; set; }
        public CompositorUrls CompositorUrls { get; set; }
        public string CountryCode { get; set; }
        public string[] CountryCodes { get; set; }
        public string CountryOfOrigin { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyCodes { get; set; }

        [JsonProperty("DECOR")]
        public string[] Decor { get; set; }

        [JsonProperty("DRIVE")]
        public string[] Drive { get; set; }
        public bool Decommissioned { get; set; }
        public int DestinationHandlingFee { get; set; }
        public int Discount { get; set; }

        [JsonProperty("discountPercentages")]
        public double DiscountPercentages { get; set; }
        public bool DisplayWarranty { get; set; }
        public DateTime? DocumentSyncDate { get; set; }
        public DateTime? EtaToCurrent { get; set; }
        public string FactoryCode { get; set; }
        public bool FixedAssets { get; set; }
        public FlexibleOptionsData[] FlexibleOptionsData { get; set; }

        [JsonProperty("HEADLINER")]
        public string[] Headliner { get; set; }
        public string HOVStatus { get; set; }
        public bool HasDamagePhotos { get; set; }
        public bool HasOptionCodeData { get; set; }

        [JsonProperty("INTERIOR")]
        public string[] Interior { get; set; }
        public string InspectionDocumentGuid { get; set; }
        public int InventoryPrice { get; set; }
        public bool IsAvailableForMatch { get; set; }
        public bool IsDemo { get; set; }
        public bool IsLegacy { get; set; }
        public bool IsSoftMatched { get; set; }
        public bool IsTegra { get; set; }
        public string Language { get; set; }
        public string[] Languages { get; set; }
        public bool LastTransformationSuccess { get; set; }
        public DateTime? LeaseAvailabilityDate { get; set; }
        public LexiconDefaultOption[] LexiconDefaultOptions { get; set; }
        public string ListingReasons { get; set; }
        public string ListingType { get; set; }
        public string ListingTypes { get; set; }
        public LoanDetails LoanDetails { get; set; }
        public string ManufacturingOptionCodeList { get; set; }
        public DateTime? MarketingInUseDate { get; set; }
        public string MatchType { get; set; }
        public string Model { get; set; }
        public int MonroneyPrice { get; set; }
        public bool NeedsReview { get; set; }
        public string OdometerType { get; set; }
        public OptionCodeData[] OptionCodeData { get; set; }
        public string OptionCodeList { get; set; }
        public string OptionCodeListDisplayOnly { get; set; }
        public OptionCodePricing[] OptionCodePricing { get; set; }
        public OrderFee OrderFee { get; set; }
        public string OriginalDeliveryDate { get; set; }
        public string OriginalInCustomerGarageDate { get; set; }

        [JsonProperty("PAINT")]
        public string[] Paint { get; set; }
        public DateTime? PlannedGADailyDate { get; set; }
        public string PreownedWarrantyEligibility { get; set; }
        public int Price { get; set; }
        public int PurchasePrice { get; set; }

        [JsonProperty("ROOF")]
        public string[] Roof { get; set; }
        public string RefurbishmentCompletionETA { get; set; }
        public string SalesMetro { get; set; }
        public string SalesNotes { get; set; }
        public string StateProvince { get; set; }
        public string StateProvinceLongName { get; set; }

        [JsonProperty("TRIM")]
        public string[] Trim { get; set; }
        public string TaxScheme { get; set; }
        public string ThirdPartyHistoryUrl { get; set; }
        public DateTime? TilburgFactoryGatedDate { get; set; }
        public DateTime? TilburgGAInDate { get; set; }
        public DateTime? TitleReceivedOwnershipTransferDate { get; set; }
        public string TitleStatus { get; set; }
        public string[] TitleSubtype { get; set; }
        public int TotalPrice { get; set; }
        public TransportFees TransportFees { get; set; }
        public string TrimCode { get; set; }
        public string TrimName { get; set; }
        public int Trt { get; set; }
        public string TrtName { get; set; }
        public DateTime? UsedVehiclePhotosCompletedDate { get; set; }

        [JsonProperty("VIN")]
        public string Vin { get; set; }
        public string Variant { get; set; }
        public string VehicleHistory { get; set; }
        public string VehicleRegion { get; set; }
        public string VrlName { get; set; }

        [JsonProperty("WHEELS")]
        public string[] Wheels { get; set; }
        public DateTime? WarrantyBatteryExpDate { get; set; }
        public bool WarrantyBatteryIsExpired { get; set; }
        public int WarrantyBatteryMile { get; set; }
        public int WarrantyBatteryYear { get; set; }
        public WarrantyData WarrantyData { get; set; }
        public DateTime? WarrantyDriveUnitExpDate { get; set; }
        public int WarrantyDriveUnitMile { get; set; }
        public int WarrantyDriveUnitYear { get; set; }
        public int WarrantyMile { get; set; }
        public DateTime? WarrantyVehicleExpDate { get; set; }
        public bool WarrantyVehicleIsExpired { get; set; }
        public int WarrantyYear { get; set; }
        public int Year { get; set; }
        public int UsedVehicleLimitedWarrantyMile { get; set; }
        public int UsedVehicleLimitedWarrantyYear { get; set; }
        public bool DeliveryDateDisplay { get; set; }
        public string Hash { get; set; }
        public int Odometer { get; set; }
        public OptionCodeSpecs OptionCodeSpecs { get; set; }
        public CompositorViewsCustom CompositorViewsCustom { get; set; }
        public bool IsRangeStandard { get; set; }
        public string MetroName { get; set; }
        public object[] geoPoints { get; set; }
        public bool HasMarketingOptions { get; set; }
        public bool IsFactoryGated { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class SearchResult
    {
        [JsonProperty("results")]
        public Vehicle[] Vehicles {get; set;}

        [JsonProperty("total_matches_found")]
        public int TotalMatchesFound {get; set;}
    }
}