//  Emailer.cs
//  http://www.mrventures.net/all-tutorials/sending-emails
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EmailSend : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txtData;
    [SerializeField] UnityEngine.UI.Button btnSubmit;
    [SerializeField] bool sendDirect;

    const string kSenderEmailAddress = "rakshitasthana17@gmail.com";
    const string kSenderPassword = "iqsvnhvrjrryagms";
    const string kReceiverEmailAddress = "rakshitasthana17@gmail.com";

    // Method 2: Server request
    const string url = "https://coderboy6000.000webhostapp.com/emailer.php";

    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(txtData);
        UnityEngine.Assertions.Assert.IsNotNull(btnSubmit);
        btnSubmit.onClick.AddListener(delegate {
            if (sendDirect)
            {
                SendAnEmail(txtData.text);
            }
            else
            {
                SendServerRequestForEmail(txtData.text);
            }
        });
    }

    // Method 1: Direct message
    private static void SendAnEmail(string message)
    {
        // Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(kSenderEmailAddress);
        mail.To.Add(kReceiverEmailAddress);
        mail.Subject = "Email Title";
        mail.Body = message;

        // Setup server 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential(
            kSenderEmailAddress, kSenderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
        }
        catch (System.Exception e)
        {
            Debug.Log("Email error: " + e.Message);
        }
        finally
        {
            Debug.Log("Email sent!");
        }
    }

    // Method 2: Server request
    private void SendServerRequestForEmail(string message)
    {
        StartCoroutine(SendMailRequestToServer(message));
    }

    // Method 2: Server request
    static IEnumerator SendMailRequestToServer(string message)
    {
        // Setup form responses
        WWWForm form = new WWWForm();
        form.AddField("name", "It's me!");
        form.AddField("fromEmail", kSenderEmailAddress);
        form.AddField("toEmail", kReceiverEmailAddress);
        form.AddField("message", message);

        // Submit form to our server, then wait
        WWW www = new WWW(url, form);
        Debug.Log("Email sent!");

        yield return www;

        // Print results
        if (www.error == null)
        {
            Debug.Log("WWW Success!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}

