using Microsoft.EntityFrameworkCore.Migrations;

namespace TollFee.Api.Migrations
{
    using System;

    public partial class InitialTollDbData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 360, 389, 9});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 390, 419, 16});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 420, 479, 22});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 480, 509, 16});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 510, 899, 9});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 900, 928, 16});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 930, 1019, 22});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 1020, 1079, 16});
            migrationBuilder.InsertData("FeeRates", new[] { "Year", "FromMinute", "ToMinute", "Price" }, new object[] { 2024, 1080, 1109, 9});
            
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 1, 1) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 1, 6) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 3, 29) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 3, 31) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 4, 1) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 5, 1) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 5, 9) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 6, 6) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 6, 22) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 1) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 2) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 3) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 4) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 5) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 6) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 7) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 8) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 9) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 10) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 11) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 12) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 13) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 14) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 15) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 16) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 17) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 18) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 19) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 20) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 21) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 22) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 23) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 24) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 25) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 26) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 27) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 28) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 29) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 30) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 7, 31) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 10, 2) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 12, 25) });
            migrationBuilder.InsertData("ZeroFeeRates", new[] { "Year", "Date" }, new object[] { 2024, new DateTime(2024, 12, 26) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
