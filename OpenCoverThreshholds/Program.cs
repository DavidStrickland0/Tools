using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OpenCoverThreshholds
{
    class Program
    {
        static void Main(string[] args)
        {
            string strCppMAX = string.Empty;
            string strBranchCoverage = string.Empty;
            string strSequenceCoverage = string.Empty;
            string filePath = string.Empty;
            if (args.Length > 0)
            {
                filePath = args[0];
            }
            if (args.Length > 1)
            {
                strCppMAX = args[1];
            }
            if (args.Length > 2)
            {
                strBranchCoverage = args[2];
            }
            if (args.Length > 3)
            {
                strSequenceCoverage = args[3];
            }
            int cppMAX;
            int.TryParse(strCppMAX, out cppMAX);
            if (cppMAX == 0) cppMAX = 10;

            int branchCoverage;
            int.TryParse(strBranchCoverage, out branchCoverage);
            if (branchCoverage == 0) branchCoverage = 80;

            int sequenceCoverage;
            int.TryParse(strSequenceCoverage, out sequenceCoverage);
            if (sequenceCoverage == 0) sequenceCoverage = 80;

            decimal actualCppMAX = 0;
            decimal actualBranchCoverage = 0;
            decimal actualSequenceCoverage = 0;

            XmlDocument doc = new XmlDocument();
            if (filePath == null || filePath.Length == 0) filePath = "outputCoverage.xml";
            doc.Load(filePath);
            XmlNode cppNode = doc.DocumentElement.SelectSingleNode("//Summary/@maxCyclomaticComplexity");
            XmlNode branchCoverageNode = doc.DocumentElement.SelectSingleNode("//Summary/@branchCoverage");
            XmlNode sequenceCoverageNode = doc.DocumentElement.SelectSingleNode("//Summary/@sequenceCoverage");

            decimal.TryParse(cppNode.Value, out actualCppMAX);
            decimal.TryParse(branchCoverageNode.Value, out actualBranchCoverage);
            decimal.TryParse(sequenceCoverageNode.Value, out actualSequenceCoverage);

            StringBuilder errorMessage = new StringBuilder();
            if(actualCppMAX!=0 && actualCppMAX> cppMAX)
            {
                errorMessage.Append("The  Cyclomatic Complexity is to high on one or more methods.");
            }
            if (actualBranchCoverage != 0 && actualBranchCoverage < branchCoverage)
            {
                errorMessage.Append("The  Branch coverage is to low one or more additional Unit Tests are required.");
            }
            else if (actualSequenceCoverage != 0 && actualSequenceCoverage < sequenceCoverage)
            {
                errorMessage.Append("The  Sequence coverage is to low one or more additional Unit Tests are required.");
            }
            if (errorMessage.Length > 0)
                throw new Exception(errorMessage.ToString());
        }
    }
}
