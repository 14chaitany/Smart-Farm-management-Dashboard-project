using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CropPrediction;
using System.Text;
using libsvm;

namespace CropPrediction
{
    public partial class Userhome : System.Web.UI.Page
    {
        static List<string> kmeansResult = new List<string>();
        MySqlConnection con = new MySqlConnection(ConfigurationManager.AppSettings["Constr"]);

       
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
           
        }
        

        protected void send(object sender, EventArgs e)
        {
            try
            {
                List<double[]> TrainingSet = new List<double[]>();
                try
                {
                    con.Open();

                    String rquery1 = "select * from cpnewdataset where Crop<>'' and Location='" + Location.Value + "' and Max_Ph='" + phvalue.Value + "';";


                    MySqlCommand cmd1 = new MySqlCommand(rquery1, con);
                    MySqlDataReader drx = cmd1.ExecuteReader();
                    if (drx.HasRows)
                    {
                        while (drx.Read())
                        {
                            double[] existingcpnewdataset = new double[] { Convert.ToDouble(drx.GetValue(21).ToString()), Convert.ToDouble(drx.GetValue(22).ToString()), Convert.ToDouble(drx.GetValue(23).ToString()), Convert.ToDouble(drx.GetValue(24).ToString()), Convert.ToDouble(drx.GetValue(25).ToString()), Convert.ToDouble(drx.GetValue(26).ToString()), Convert.ToDouble(drx.GetValue(27).ToString()), Convert.ToDouble(drx.GetValue(28).ToString()), Convert.ToDouble(drx.GetValue(29).ToString()), Convert.ToDouble(drx.GetValue(30).ToString()), Convert.ToDouble(drx.GetValue(31).ToString()) };
                            TrainingSet.Add(existingcpnewdataset);
                        }
                    }
                    drx.Close();

                    double[] newcpnewdataset = new double[] { Convert.ToDouble(lat.Value), Convert.ToDouble(lon.Value), Convert.ToDouble(phvalue.Value), Convert.ToDouble(lat.Value), Convert.ToDouble(lon.Value), Convert.ToDouble(phvalue.Value), Convert.ToDouble(lat.Value), Convert.ToDouble(lon.Value), Convert.ToDouble(phvalue.Value), Convert.ToDouble(lat.Value), Convert.ToDouble(lon.Value) };

                    //double distance = 0.0;

                    //SVM
                    int counter = 0;
                    List<string> x = new List<string>();

                    double[] y = null;
                    String rquery = "Select Count(*) from cpnewdataset";
                    MySqlCommand cmdx = new MySqlCommand(rquery, con);
                    counter = Convert.ToInt32(cmdx.ExecuteScalar());
                    if (counter > 0)
                    {
                        y = new double[counter];
                        rquery = "Select Crop from cpnewdataset";
                        cmdx = new MySqlCommand(rquery, con);
                        drx = cmdx.ExecuteReader();
                        if (drx.HasRows)
                        {
                            int i = 0;
                            while (drx.Read())
                            {
                                x.Add(drx.GetValue(0).ToString());
                                y[i] = 1;
                            }
                        }
                        drx.Close();



                        var wineSet = x.SelectMany(GetDatas).Distinct().OrderBy(word => word).ToList();
                        var w = (IReadOnlyList<string>)wineSet;
                        var problemBuilder = new SVMClassificationProblemBuilder();
                        var problem = problemBuilder.CreateProblem(x, y, w.ToList());

                        // If you want you can save this problem with : 
                        // ProblemHelper.WriteProblem(@"D:\MACHINE_LEARNING\SVM\Tutorial\sunnyData.problem", problem);
                        // And then load it again using:
                        // var problem = ProblemHelper.ReadProblem(@"D:\MACHINE_LEARNING\SVM\Tutorial\sunnyData.problem");

                        const int C = 1;
                        var model = new C_SVC(problem, KernelHelper.LinearKernel(), C);


                        Random r = new Random();
                        if (counter > 0)
                        {
                            double[][] rawData = new double[counter][];
                            rquery = "Select Crop from cpnewdataset";
                            cmdx = new MySqlCommand(rquery, con);
                            drx = cmdx.ExecuteReader();
                            if (drx.HasRows)
                            {
                                int i = 0;
                                while (drx.Read())
                                {
                                    double val1 = 0;
                                    double val2 = 0;
                                    if (r.Next(0, 9) % 2 == 0)
                                        val1 = 0;
                                    if (r.Next(0, 9) % 2 != 0)
                                        val2 = 3;
                                    rawData[i] = new double[] { val1, val2 };
                                    i++;
                                }
                            }
                            drx.Close();


                            //KMeans
                            MachineLearning.Process mp = new MachineLearning.Process();
                            ShowData(rawData, 1, true, true);
                            int numClusters = 2;
                            int[] clustering = Cluster(rawData, numClusters);
                            ShowVector(clustering, true);
                            ShowClustered(rawData, clustering, numClusters, 1);

                            //Response.Redirect("index.aspx");
                            // Resetter();




                            //J48
                            J48 j = new J48();
                            List <String> dataset= new List<string>();
                            rquery = "Select Crop from cpnewdataset";
                            cmdx = new MySqlCommand(rquery, con);
                            drx = cmdx.ExecuteReader();
                            if (drx.HasRows)
                            {
                                int i = 0;
                                while (drx.Read())
                                {
                                    double val1 = 0;
                                    double val2 = 0;
                                    if (r.Next(0, 9) % 2 == 0)
                                        val1 = 0;
                                    if (r.Next(0, 9) % 2 != 0)
                                        val2 = 3;
                                    rawData[i] = new double[] { val1, val2 };
                                    i++;
                                    dataset.Add(drx.GetValue(0).ToString());
                                }
                            }
                            drx.Close();

                            j.ProcessDataset(dataset);



                            //Linear Regrssion
                            List<double> yearvals = new List<double>();
                            List<double> revvals = new List<double>();

                            rquery = "Select distinct Year,Investment from cpnewdataset group by Year";
                            cmdx = new MySqlCommand(rquery, con);
                            drx = cmdx.ExecuteReader();
                            if (drx.HasRows)
                            {
                                yearvals.Add(Convert.ToDouble(drx.GetValue(0).ToString()));
                                revvals.Add(Convert.ToDouble(drx.GetValue(1).ToString()));
                            }
                            drx.Close();

                            var xValues = new double[yearvals.Count];
                            for (int i=0;i<yearvals.Count;i++)
                            {
                                xValues[i] = yearvals[i];
                            }


                            var yValues = new double[revvals.Count];
                            for (int i = 0; i < revvals.Count; i++)
                            {
                                yValues[i] = revvals[i];
                            }

                            double rSquared, intercept, slope;
                            LinearRegression(xValues, yValues, out rSquared, out intercept, out slope);

                            Console.WriteLine($"R-squared = {rSquared}");
                            Console.WriteLine($"Intercept = {intercept}");
                            Console.WriteLine($"Slope = {slope}");

                            var predictedValue = (slope * yearvals[yearvals.Count-1]) + intercept;

                        }
                    }
                }
                catch
                {

                }
            }
            catch
            {

            }
            Locator l = new Locator();
            string loc=l.Location(lat.Value, lon.Value);
            double landval = Convert.ToDouble(land.Value);
            double phval = Convert.ToDouble(phvalue.Value);

            Location.Value = loc;

            Panel1.Visible = true;
            //BindData();

            con.Close();
            con.Open();
            String lati = lat.Value;
            String loni = lon.Value;
            double upLat = Convert.ToDouble(lati) + 0.0512;
            double downLat = Convert.ToDouble(lati) - 0.0512;
            double upLon = Convert.ToDouble(loni) + 0.0512;
            double downLon = Convert.ToDouble(loni) - 0.5123;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Crop", typeof(string)),
                            new DataColumn("Investment", typeof(string)),
                            new DataColumn("Profit", typeof(string)),
                            new DataColumn("SeedsPerAcre", typeof(string)),
                            new DataColumn("FertilizersPerAcre", typeof(string)),
                            new DataColumn("ApproxInvestment", typeof(string)) });            
            string query = "Select Crop,Investment,Profit,Seeds_Per_Hectare,Fetilizers_per_hectare From  cpnewdataset Where Location='" + loc+ "' and CONVERT(Max_Ph,Decimal)<='"+ phval + "' and Year BETWEEN '2017' AND '2023'  LIMIT 50 ";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dt.Rows.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString()+" Rs", dr.GetValue(2).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), Convert.ToString(Convert.ToDouble(dr.GetValue(1).ToString())*landval)+" Rs");
                }
            }
            dr.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();


            Panel2.Visible = true;
            BindChart();

        }

        private void BindChart()
        {
            System.Data.DataTable dsChartData = new System.Data.DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = GetChartData();

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Crop', 'Revenue'],");


                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["Crop"] + "'," + row["Counter"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { backgroundColor: 'transparent',title : 'Overall Crop revenue',vAxis:  {title: 'Revenue in RS'},hAxis:  {title: 'Crops'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltScripts.Text = strScript.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // dsChartData.Dispose();
            }
        }


        private System.Data.DataTable GetChartData()
        {
            DataSet dsData = new DataSet();
            try
            {
                MySqlConnection sqlCon = new MySqlConnection(ConfigurationManager.AppSettings["Constr"]);
                //SqlDataAdapter sqlCmd = new SqlDataAdapter("GetData", sqlCon);
                //sqlCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                string queryString = "Select distinct Crop, Sum(Profit) as Counter from cpnewdataset where Year BETWEEN '2017' AND '2023' and Location='" + Location.Value + "' and CONVERT(Max_Ph,Decimal)<='" + phvalue.Value + "' group by Crop";
                MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, sqlCon);
                sqlCon.Open();

                adapter.Fill(dsData);

                sqlCon.Close();
            }
            catch
            {
                throw;
            }
            return dsData.Tables[0];
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private static IReadOnlyList<string> GetDatas(string x)
        {
            return x.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        }
        protected void btnReset(object sender, EventArgs e)
        {
            Response.Redirect("userpage.aspx");
        }




        public static int[] Cluster(double[][] rawData, int numClusters)
        {
            double[][] data = Normalized(rawData);
            bool changed = true; bool success = true;
            int[] clustering = InitClustering(data.Length, numClusters, 0);
            double[][] means = Allocate(numClusters, data[0].Length);
            int maxCount = data.Length * 10;
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                success = UpdateMeans(data, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }
            return clustering;
        }
        private static double[][] Normalized(double[][] rawData)
        {
            // normalize raw data by computing (x - mean) / stddev
            // primary alternative is min-max:
            // v' = (v - min) / (max - min)

            // make a copy of input data
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        private static int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            // init clustering semi-randomly (at least one tuple in each cluster)
            // consider alternatives, especially k-means++ initialization,
            // or instead of randomly assigning each tuple to a cluster, pick
            // numClusters of the tuples as initial centroids/means then use
            // those means to assign each tuple to an initial cluster.
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i) // make sure each cluster has at least one tuple
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters); // other assignments random
            return clustering;
        }

        private static double[][] Allocate(int numClusters, int numColumns)
        {
            // convenience matrix allocator for Cluster()
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numColumns];
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            // returns false if there is a cluster that has no tuples assigned to it
            // parameter means[][] is really a ref parameter

            // check existing cluster counts
            // can omit this check if InitClustering and UpdateClustering
            // both guarantee at least one tuple in each cluster (usually true)
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to means[][]

            // update, zero-out means so it can be used as scratch matrix 
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j]; // accumulate sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            // (re)assign each tuple to a cluster (closest mean)
            // returns false if no tuple assignments change OR
            // if the reassignment would result in a clustering where
            // one or more clusters have no tuples.

            int numClusters = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length]; // proposed result
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters]; // distances from curr tuple to each mean

            for (int i = 0; i < data.Length; ++i) // walk thru each tuple
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]); // compute distances from curr tuple to all k means

                int newClusterID = MinIndex(distances); // find closest mean ID
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID; // update
                }
            }

            if (changed == false)
                return false; // no change so bail and don't update clustering[][]

            // check proposed clustering[] cluster counts
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to clustering[][]

            Array.Copy(newClustering, clustering, newClustering.Length); // update
            return true; // good clustering and at least one change
        }

        private static double Distance(double[] tuple, double[] mean)
        {
            // Euclidean distance between two vectors for UpdateClustering()
            // consider alternatives such as Manhattan distance
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {
            // index of smallest value in array
            // helper for UpdateClustering()
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        // ============================================================================

        // misc display helpers for demo

        static void ShowData(double[][] data, int decimals, bool indices, bool newLine)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (data[i][j] >= 0.0) Console.Write(" ");
                    kmeansResult.Add(data[i][j].ToString("F" + decimals) + " ");
                }
                kmeansResult.Add("");
            }
            if (newLine) kmeansResult.Add("");
        } // ShowData

        static void ShowVector(int[] vector, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            if (newLine) Console.WriteLine("\n");
        }

        static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals)
        {
            for (int k = 0; k < numClusters; ++k)
            {

                for (int i = 0; i < data.Length; ++i)
                {
                    int clusterID = clustering[i];
                    if (clusterID != k) continue;
                    kmeansResult.Add(i.ToString().PadLeft(3) + " ");
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        if (data[i][j] >= 0.0) Console.Write(" ");
                        kmeansResult.Add(data[i][j].ToString("F" + decimals) + " ");
                    }
                    kmeansResult.Add("");
                }
            } // k
        }














        public static void LinearRegression( double[] xVals,   double[] yVals,   out double rSquared,    out double yIntercept,         out double slope)
        {
            if (xVals.Length != yVals.Length)
            {
                throw new Exception("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < xVals.Length; i++)
            {
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            var count = xVals.Length;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);

            rSquared = dblR * dblR;
            yIntercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }
    }
}