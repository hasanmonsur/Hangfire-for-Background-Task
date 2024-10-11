namespace HangfireDemo.Services
{
    public class MyBackgroundJob
    {
        public void Execute()
        {
            // Background job logic here
            Console.WriteLine("Executing background job at: " + DateTime.Now);
        }
    }
}
