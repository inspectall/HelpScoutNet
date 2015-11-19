# HelpScoutNet
HelpScoutNet is a .NET class library that provides an easy-to-use interface for the helpscout.net web api

##Methods Implemented

###Help Desk API

* Conversation
    * List Conversations
    * Get Conversations
    * Get Attachment Data
    * Create Conversation
    * Update Conversation
    * Create Thread
    * Delete Conversation
    * Delete Note
    * Create Attachment
    * Delete Attachment
* Customers
   * List Customers
   * List Mailbox Customers
   * Get Customer
   * Create Customer
   * Update Customer
* Mailboxes
    * List Mailboxes
    * Get Mailbox
    * Get Folders
* Search
    * Conversations
    * Customers
* Tags
    * List Tags
* Users
    * List Users
    * Get User
    * List Users ny Mailbox
* Workflows
    * List Workflows
* Reports
   * Busy Time Statistics
   * Customer Statistics
   * Handle Time Statistics
   * Multiple Time Range Statistics (Conversations)
   * Multiple Time Range Statistics (Productivity)
   * Multiple Time Range Statistics (Team)
   * Multiple Time Range Statistics (User)
   * New Conversation Statistics
   * Replies to Resolve Statistics
   * Response Time Statistics
   * Saved Reply Statistics
   * Tag Statistics
   * Time Range Statistics (Conversations)
   * Time Range Statistics (Productivity)
   * Time Range Statistics (Team)
   * Time Range Statistics (User)
   * User Statistics (Team)
   * Workflow Statistics


###Docs API

* Article
    * List Articles
    * Get Article
* Category
    * List Categories
    * Get Category
* Collection
    * List Collections
    * Get Collection
* Site
    * List Sites
    * Get Site

##Examples 

###Initialization of the client
```csharp
var client = new HelpScoutClient(ApiKey);
```
###Search customers
```csharp

var client = new HelpScoutClient(ApiKey);
var customersSearch = client.SearchCustomers(new SearchRequest{ Query = "(customer:\"johnappleseed@gmail.com\")"});
foreach (var searchresult in customersSearch.Items)
{
    Console.WriteLine(searchresult.FirstName + searchresult.LastName);   
}

```

###List Mailboxes
```csharp

var mailboxes = client.ListMailboxes();
foreach (var mailboxStub in mailboxes.Items)
{    
    Console.WriteLine(mailboxStub.Id);
}
  
```

###Create conversation
```csharp
var newConv = client.CreateConversation(new Conversation
                {
                    Type = ConversationType.email,
                    Subject = "Testing the Helpscout API Mathieu",
                    Mailbox = new MailboxRef
                    {
                        Id = 38556
                    },
                    Status = ConversationStatus.active,
                    Customer = new Person
                    {
                        Email = "johnappleseed@somemail.com",
                    },

                    Threads = new List<Thread>{
                        new Thread
                        {
                            Type = ThreadType.message,
                            Body = "This is the body of the email \n something else" + Environment.NewLine + "and again",
                            Status = ThreadStatus.active,
                            CreatedBy = new Person
                            {    
                                Id = 60895,
                                Type = PersonType.user,
                                Email = "mathieu@somemail.com"
                            } 
                        }
                     }

                });

```
### Delete a conversation
```csharp
client.DeleteConversation(111947647);
```

###Add a note, create thread
```csharp
var thread = client.CreateThread(newconv.Id, new Thread
                {
                    CreatedBy = new Person
                    {
                        Id = 60895,
                        Type = PersonType.user,
                        Email = "mathieu@somemail.com"
                    },
                    Type = ThreadType.note,
                    Body = "This is a note from API",
                    Status = ThreadStatus.active
                });
```

###Create an attachment
```csharp

Byte[] bytes = File.ReadAllBytes(@"C:\Users\mathieu.kempe.SELZ\Desktop\sift-logo.png");
String file = Convert.ToBase64String(bytes);

string fileHash = client.CreateAttachment(new CreateAttachmentRequest
                                          {
                                             FileName = "sift-logo.png",
                                             MimeType = "image/png",
                                             Data = file
                                          });

```

To add the attachment to a new conversation

```csharp
 var newConv = client.CreateConversation(new Conversation
  {
      ...
      Threads = new List<Thread>{
                        new Thread
                        {
                            ...
                            Attachments = new List<Attachment>
                            {
                                new Attachment
                                {
                                    Hash = fileHash
                                }
                            }
                        }
                     }
               });
               
```


###Field Selectors 

Each endpoint returns a default set of fields based upon the given request. However, you can override this behavior by supplying one or more field selectors to explicitly request the data you need.

Instead of returning a complete customer object, you could return just the ID and lastname.

```csharp

client.GetCustomer(123, new CustomerRequest {Fields = new[] {"id", "lastName"}});
  
```
