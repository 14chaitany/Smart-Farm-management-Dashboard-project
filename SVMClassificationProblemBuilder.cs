using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CropPrediction
{
    internal class SVMClassificationProblemBuilder
    {
        public svm_problem CreateProblem(IEnumerable<string> x, double[] y, IReadOnlyList<string> deferralSet)
        {
            return new svm_problem
            {
                y = y,
                x = x.Select(xVector => CreateCluster(xVector, deferralSet)).ToArray(),
                l = y.Length
            };
        }

        public static svm_node[] CreateCluster(string x, IReadOnlyList<string> deferralSet)
        {
            var node = new List<svm_node>(deferralSet.Count);

            string[] lister = x.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < deferralSet.Count; i++)
            {
                int occurenceCount = lister.Count(s => String.Equals(s, deferralSet[i], StringComparison.OrdinalIgnoreCase));
                if (occurenceCount == 0)
                    continue;

                node.Add(new svm_node
                {
                    index = i + 1,
                    value = occurenceCount
                });
            }

            return node.ToArray();
        }
    }
}
