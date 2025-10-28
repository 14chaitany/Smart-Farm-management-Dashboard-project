
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace CropPrediction
{
    internal class J48
    {
        
            public J48()
            {

            }

            public Node Process(List<Model> rows, List<string> attributes, string branchLabel)
            {
                if (AllPositive(rows))
                {
                    return new Node() {Value = branchLabel };
                }
                if (AllNegative(rows))
                {
                    return new Node() { Value = branchLabel };
                }
                if (attributes == null)
                {
                    return new Node() { Label = MostPosOrNeg(rows), Value = branchLabel };
                }
                Node root = new Node();
                GainTuple gt = MaxGainAttribute(rows, attributes);
                root.Attribute = gt.attribute;
                root.Gain = gt.gain;
                root.Value = branchLabel;
                foreach (string value in possibleValues(rows, gt.attribute))
                {
                    List<Model> rest = null;
                    if (rest.Count == 0)
                    {
                        root.children.Add(new Node() { Label = MostPosOrNeg(rows), Value = value });
                    }
                    else
                    {
                        root.children.Add(Process(rest, attributes.Except(new List<string>() { root.Attribute }).ToList(), value));
                    }

                }
                return root;
            }

            private string MostPosOrNeg(List<Model> rows)
            {
                int pos = rows.Count();
                int neg = rows.Count - pos;
                if (pos > neg) { return "0"; }
                else if (neg > pos) { return"1"; }
                else { return String.Format("{0} and {1} Equally Likely", "0", "1"); }

            }

        public string ProcessDataset(List<String> rows)
        {
            int pos = rows.Count();
            int neg = rows.Count - pos;
            if (pos > neg) { return "0"; }
            else if (neg > pos) { return "1"; }
            else { return String.Format("{0} and {1} Equally Likely", "0", "1"); }

        }


        /// <summary>
        /// If all rows in a set contains a 'successful' outcome according to Model.Success()
        /// </summary>
        /// <param name="rows">the set of rows</param>
        /// <returns>true if all are positive</returns>
        private bool AllPositive(List<Model> rows)
            {
                foreach (Model row in rows)
                {
                     { return false; }
                }
                return true;
            }

            private bool AllNegative(List<Model> rows)
            {
                foreach (Model row in rows)
                {
                    { return false; }
                }
                return true;
            }


            private GainTuple MaxGainAttribute(List<Model> rows, List<string> attrs)
            {
                List<GainTuple> gains = new List<GainTuple>();
                foreach (string attr in attrs)
                {
                    gains.Add(new GainTuple { gain = Gain(rows, attr), attribute = attr });
                }
                return gains.OrderByDescending(x => x.gain).First();
            }

            private double Prob(List<Model> set, Func<Model, bool> predicate)
            {
                double tot = set.Where(x => predicate(x)).Count();
                double selection = Convert.ToDouble(set.Where(x => predicate(x)));
                return selection / tot;
            }

            private double Entropy(List<Model> set, Func<Model, bool> predicate)
            {
                double prob = Prob(set, predicate);
                double e;
                if (prob == 0) { e = 0; }
                else if (prob == 1) { e = 0; }
                else
                {
                    e = e_vlad(prob);
                }
                return e;
            }

            private double e_vlad(double prob)
            {
                return (prob * Math.Log(1 / prob, 10)) + ((1 - prob) * Math.Log(1 / (1 - prob), 10));
            }

            private double e_internet(double prob)
            {
                return -(prob * Math.Log(prob, 2)) - ((1 - prob) * Math.Log((1 - prob), 2));
            }



            private double Gain(List<Model> set, string attr)
            {
                double g = 0;
                List<string> values = possibleValues(set, attr);

                // Entropy for the whole set.
                g += Entropy(set, (x => true));
                //The entropies of the classes of the attribute   
                
                //Console.WriteLine()
                return g;
            }

            private List<string> possibleValues(List<Model> set, string attr)
            {
                List<string> values = new List<string>();
                values = values.Select(x => x).Distinct().ToList();
                return values;
            }

            class GainTuple { public double gain; public string attribute; }
        }
    }





