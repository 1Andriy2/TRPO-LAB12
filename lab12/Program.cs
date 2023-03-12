using System.Diagnostics;

int n = 6;
int m = 20;
int l = 30;
int valbuilt = 1003;
double[,] A = new double[m, n];
double[,] B = new double[n, l];
double[,] C = new double[m, l];

for (int i = 1; i <= m; i++)
{
    for (int j = 1; j <= n; j++)
    {
        A[i - 1, j - 1] = (Math.Pow(i, 3)+1) / (i + Math.Pow(Math.Cos(Math.Pow(j,0.3)),2)) + j * j;
    }
}
for (int i = 1; i <= n; i++)
{
    for (int j = 1; j <= l; j++)
    {
        B[i - 1, j - 1] = (Math.Pow(3,j)-0.3+1)/(j+Math.Pow(Math.Cos(Math.Pow((5*j),0.3)),2));
    }
}

int count = 0;
Stopwatch stopwatch1;

while (count < 10)
{
    stopwatch1 = new();
    stopwatch1.Start();
    BasicAlgorithm(A, B, C, m, n, l);
    stopwatch1.Stop();

    Console.WriteLine("Execution time of the basic algorithm with m = {0}, n = {1} and l = {2} : {3}", n, m,l, stopwatch1.Elapsed.TotalMilliseconds);
    
    if (count < 4)
    {
        m += valbuilt;
        A = new double[m, n];
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                A[i - 1, j - 1] = (Math.Pow(i, 3) + 1) / (i + Math.Pow(Math.Cos(Math.Pow(j, 0.3)), 2)) + j * j;
            }
        }
    }
    else
    {
        l += valbuilt;
        B = new double[n, l];
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= l; j++)
            {
                B[i - 1, j - 1] = (Math.Pow(3, j) - 0.3 + 1) / (j + Math.Pow(Math.Cos(Math.Pow((5 * j), 0.3)), 2));
            }
        }
    }
    C = new double[m, l];
    count++;
}


void BasicAlgorithm(double[,] Matrix1, double[,] Matrix2, double[,] Result, int rows, int coln, int coll)
{
    Task[] tasks = new Task[rows];
    for (int i = 0; i < rows; i++)
    {
        int row = i;
        tasks[i] = Task.Factory.StartNew(() =>
        {
            for (int j = 0; j < coll; j++)
            {
                for (int k = 0; k < coln; k++)
                {
                    Result[row, j] += A[row, k] * B[k, j];
                }
            }
        });
    }
    Task.WaitAll(tasks);
}