using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CarFuel.Models;

namespace CarFuel.Facts.Models {
    public class CarFacts {
        public class GeneralUsage {

            [Fact]
            public void NewCar_HasCorrectInitialValues() {
                Car c = new Car();
                c.Make = "Honda";
                c.Model = "Accord";
                Assert.Equal("Honda",c.Make);
                Assert.Equal("Accord",c.Model);
                Assert.Equal(0,c.FillUps.Count());
               
                }
            }

        public class AddFillUpmethod {

            [Fact]
            public void CanAddNewFillUp() {
                var c = new Car();
                FillUp f = c.AddFillUp(odometer: 1000,liters: 50.0,isFull: true);

                Assert.Equal(1,c.FillUps.Count());
                Assert.Equal(1000,f.Odometer);
                Assert.Equal(50.0,f.Liters);
                Assert.True(f.IsFull);
                }

            [Fact]
            public void AddTwoFillUps() {
                // add f1
                // add f2
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000,liters: 50.0,isFull: true);
                FillUp f2 = c.AddFillUp(odometer: 1600,liters: 60.0,isFull: true);
                FillUp f3 = c.AddFillUp(odometer: 2000,liters: 50.0,isFull: true);

                // assert that c has 2 fill ups
                // assert that f1.NextFillUp is f2
                Assert.Equal(3,c.FillUps.Count());
                Assert.Same(f2,f1.NextFillUp);
                Assert.Same(f3,f2.NextFillUp);

                Assert.Same(f1,f2.PreviousFillUp);
                Assert.Same(f2,f3.PreviousFillUp);

                }


            }
        public class AverageKilometersPerLites {
            [Fact]
            public void NoFillUp_IsNull() {
            var c = new Car();
            Assert.Null(c.AverageKilometersPerListes);
                }

            [Fact]
            public void SingleFillUp_IsNull() {
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000,liters: 50.0,isFull: true);
                Assert.Null(c.AverageKilometersPerListes);
                }

            [Fact]
            public void TwoFillUp_IsNull() {
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000,liters: 50.0,isFull: true);
                FillUp f2 = c.AddFillUp(odometer: 1600,liters: 60.0,isFull: true);

                Assert.Equal(10,c.AverageKilometersPerListes);
                }

            [Fact]
            public void ThreeFillUps() {
                var c = new Car();
                FillUp f1 = c.AddFillUp(odometer: 1000,liters: 50.0,isFull: true);
                FillUp f2 = c.AddFillUp(odometer: 1600,liters: 60.0,isFull: true);
                FillUp f3 = c.AddFillUp(odometer: 2000,liters: 50.0,isFull: true);

                var kml = c.AverageKilometersPerListes;
                Assert.Equal(9.1,kml);
                }
            }
       
        }
    }
