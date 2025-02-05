using System;
using System.Threading.Tasks;
using Almirah.Services.Interfaces;
using Foundation;
using LocalAuthentication;

namespace Almirah.Mac;

public class AuthService : IAuthService
{
    public async Task<bool> AuthenticateAsync()
    {
        var context = new LAContext();
        NSError error = null;

        // Check if biometrics are available
        if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error))
        {
            // Prompt the user for authentication
            var reason = new NSString("Authenticate using Touch ID or Face ID");

            // We need to call EvaluatePolicy asynchronously and handle the result in a completion handler
            var tcs = new TaskCompletionSource<bool>();

            context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, reason, (success, evaluationError) =>
            {
                if (success)
                {
                    tcs.SetResult(true);  // Authentication succeeded
                }
                else
                {
                    tcs.SetResult(false); // Authentication failed
                    Console.WriteLine($"Authentication failed: {evaluationError?.LocalizedDescription}");
                }
            });

            // Await the result of the TaskCompletionSource
            return await tcs.Task;
        }
        else
        {
            // Handle the case where biometric authentication is not available
            Console.WriteLine("Biometric authentication is not available.");
            return false;
        }
    }
}
