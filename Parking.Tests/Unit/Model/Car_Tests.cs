using Parking.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Tests.Unit.Model
{
    class Car_Tests
    {
        public class Tests
        {
            private List<ValidationResult> ValidateModel(Car car)
            {
                var context = new ValidationContext(car, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                Validator.TryValidateObject(car, context, results, validateAllProperties: true);
                return results;
            }

            [Fact]
            public void Car_WithValidData_ShouldBeValid()
            {
                var car = new Car
                {
                    LicensePlate = "А123ВС777",
                    Brand = "Toyota",
                    Model = "Camry"
                };

                var results = ValidateModel(car);

                Assert.Empty(results); // Нет ошибок валидации
            }

            [Fact]
            public void Car_MissingLicensePlate_ShouldBeInvalid()
            {
                var car = new Car
                {
                    Brand = "Toyota",
                    Model = "Camry"
                };

                var results = ValidateModel(car);

                Assert.Contains(results, v => v.MemberNames.Contains("LicensePlate"));
            }

            [Fact]
            public void Car_LicensePlateTooLong_ShouldBeInvalid()
            {
                var car = new Car
                {
                    LicensePlate = new string('A', 16),
                    Brand = "Toyota",
                    Model = "Camry"
                };

                var results = ValidateModel(car);

                Assert.Contains(results, v => v.MemberNames.Contains("LicensePlate") && v.ErrorMessage!.Contains("длиннее 15 символов"));
            }

            [Fact]
            public void Car_MissingBrand_ShouldBeInvalid()
            {
                var car = new Car
                {
                    LicensePlate = "А123ВС777",
                    Model = "Camry"
                };

                var results = ValidateModel(car);

                Assert.Contains(results, v => v.MemberNames.Contains("Brand"));
            }

            [Fact]
            public void Car_ModelTooLong_ShouldBeInvalid()
            {
                var car = new Car
                {
                    LicensePlate = "А123ВС777",
                    Brand = "Toyota",
                    Model = new string('M', 101)
                };

                var results = ValidateModel(car);

                Assert.Contains(results, v => v.MemberNames.Contains("Model"));
            }
        }


    }
}
