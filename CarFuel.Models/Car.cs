using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFuel.Models {
    public class Car {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [StringLength(30)]
        public string Make { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [StringLength(30)]
        public string PlateNo { get; set; }

        public virtual ICollection<FillUp> FillUps { get; set; }

        public Car() {
            FillUps = new HashSet<FillUp>();
            }

        public FillUp AddFillUp(int odometer
                               ,double liters
                               ,bool isFull = true
                               ,bool isForgot = false) {

            //  throw new NotImplementedException();
            FillUp thisFillUp = new FillUp();
            thisFillUp.Odometer = odometer;
            thisFillUp.Liters = liters;
            thisFillUp.IsFull = isFull;
            thisFillUp.IsForgot = isForgot;

            var previousFillUp = this.FillUps.LastOrDefault();
            if(previousFillUp != null) {
                previousFillUp.NextFillUp = thisFillUp;
                thisFillUp.PreviousFillUp = previousFillUp;
                }

            this.FillUps.Add(thisFillUp);
            return thisFillUp;
            }
        public double? AverageKilometersPerListes {
            get {
                if(FillUps.Count() < 2) {
                    return null;
                    }
                FillUp first;
                FillUp last = FillUps.Last();
                double sumKML = 0.0;
                int blocks = 0;

                last = FillUps.Last();
                //   first = FillUps.First();
                do {
                    while(last.IsForgot && last.PreviousFillUp != null) {
                        last = last.PreviousFillUp;
                        }
                    first = last;
                    double liters = 0.0;
                    while(first.PreviousFillUp != null) {
                        liters += first.Liters;
                        first = first.PreviousFillUp;
                        if(first.IsForgot) break;
                        }

                    var distanct = last.Odometer - first.Odometer;
                    if(liters > 0) {
                        var kml = Math.Round(distanct / liters,1,MidpointRounding.AwayFromZero);
                        sumKML += kml;
                        blocks++;
                        }
                    last = first.PreviousFillUp;
                    } while(last != null);

                return Math.Round(sumKML / blocks,1,MidpointRounding.AwayFromZero);
                }
            }
        }
    }
