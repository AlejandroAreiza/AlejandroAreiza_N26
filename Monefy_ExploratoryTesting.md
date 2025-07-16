# Monefy - Budget & Expenses app
- [Monefy - Budget \& Expenses app](#monefy---budget--expenses-app)
  - [Exploratory Testing - Charter Based Testing](#exploratory-testing---charter-based-testing)
      - [Test Context](#test-context)
      - [Charter 1: Summary Ring Detail Screen](#charter-1-summary-ring-detail-screen)
      - [Charter 2: Add Expense/Income Slide-up Panel](#charter-2-add-expenseincome-slide-up-panel)
      - [Charter 3: Transfer feature in Navigation ribbon](#charter-3-transfer-feature-in-navigation-ribbon)
      - [Charter 4: Account Switching in Navigation ribbon](#charter-4-account-switching-in-navigation-ribbon)
      - [Charter 5: Categories Management - Settings Panel](#charter-5-categories-management---settings-panel)
      - [Charter 6: Accounts Management - Settings Panel](#charter-6-accounts-management---settings-panel)
      - [Charter 7: Integration of Category Management screen and Summary Ring](#charter-7-integration-of-category-management-screen-and-summary-ring)
      - [Charter 8:  Integration of Account Management Screen and Summary Ring Detail](#charter-8--integration-of-account-management-screen-and-summary-ring-detail)
  - [Risk And Mitigation](#risk-and-mitigation)

---
## Exploratory Testing - Charter Based Testing

#### Test Context

The mission of this exploratory testing is to assess the overall quality of the Monefy mobile application by exploring its main features and identifying bugs or unexpected behaviors at this stage. This helps to ensure that the app works as expected and supports future development with fewer risks.

- **Tester**: Alejando areiza
- **Device**: Device with Android version 5.0 +
- **App Name**: Monefy
- **Language**: English
- **App Version**: 1.22.2
- **Test Type**: Exploratory (Functional + UI)
- **Date**: 2025-07-11
  
---

#### Charter 1: Summary Ring Detail Screen
- **Goal:** Explore the Summary Ring Detail Screen using expense/income flow to discover how UI components update and reflect changes correctly
- **Set Up:** Categories and Accounts already created
- **Priority:** High -> This screen is the core financial overview for users. Any inaccuracies or UI failures here directly impact user trust and decision-making, making it critical to ensure flawless data reflection and interaction.
- **Scope:** Ring summary screen, expense/income slide-up panel, and balance container
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 20 minutes
- **Risks/Assumptions:**
    - Incorrect update of balance or summary ring after saving
    - Invalid amounts accepted (e.g. 0, negative)
    - Incorrect category assignment
    - UI only vertical mode
- **Test Ideas:**
    | Test Idea | Expected Behavior |
    | - | - |
    | Add a valid expense of 100€ with category House | Expense saved, summary rings updates, balance decreases and percentage is reflected by category |
    | Select a category and add a expense of 99.5$ | Expense saved with floats, summary rings updates, balance decreases and percentage is reflected in the chosen category |
    | Add an income with a 0 or negative amount | Input rejected or validation error |
    | Add a large number in expense | Input accepted and summary ring widget does not break|
    | Add a valid income of 3000€ with category Salary | Income saved, summary rings update and balance increases |
    | Add a valid income of 50€ with non category | Validation Error about required category|
    | Add multiple expenses in a row | Performance of the application should not be affected and expenses should be succesfully saved|
    | Rotate device | Application should remain in vertical mode |

- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question? |
    | - | - |- |
    | adding a valid expense | Expense saved, summary rings updates, balance decreases and percentage is reflected by category | ok |
    | adding valid expenses with float amount | expense sucessfully saved | ok - only two decimals allowed |
    | 0 or negative amounts | 0 or negative amounts not allowed | ok - Input validation error |
    | large amounts| large amounts allowed | ok - Amount input only allows up to 8 digits |
    | adding a valid income| Income saved, summary rings update and balance increases| ok |
    | Amounts with non category | Input Validation Error by category mismatch | Category is a required field, new categories is a paid service|
    | Add multiple expenses in a row| expenses are saved correctly | ok - application didnt' break, performance was not affected|
    | Rotate screen | app only works in vertical mode | ok |

    | Findings |
    | - |
    | There is a calculator when adding expsenses or incomes |
    | Icons are preloaded and organized automatically based on percentage |
    | Sliding to previews/forwards dates is allowed from the Summary Ring Screen|
    | Pressing over the icon reflects the total value and percentage within the ring widget|
    | You can add expenses by clicking on the icon|
    | Adding several expenses to same category is allowed |
    | Adding notes to the expenses and income is allowed, notes can have emojis|



---

#### Charter 2: Add Expense/Income Slide-up Panel
- **Goal:** Explore the Add Expense/Income slide-up panel using filters and input forms to discover how balance and transactions grouping behave
- **Set Up:** Categories and Accounts already created
- **Priority:** Medium -> This feature is essential for accurate transaction history and audit trails. While the app remains functional if it malfunctions, any errors here can degrade data integrity and user experience significantly over time.
- **Scope:** Expense/Income slide-up panel, Balance container and Category/Date grouping
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 20 minutes
- **Risks/Assumptions:**
  - Balance increases or decreases incorrectly
  - Filter by date is not working as expected
  - UI only vertical mode
  - Similar bugs as with summary ring widget screen
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Add a valid expense of 45€ with category Food | Expense saved by category and balance decreases |
    | Add an income with a 0 or negative amount | Input rejected or validation error |
    | Add a valid income with a large note | Saved properly and notes is wrapped correctly |
    | Add a duplicate income | Warning message about a duplicate income |
    | Select filter by date | Income and expenses are filter correctly by the created date |
    | Duplicate an existing expense | duplication of expenses is allowed |


- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | add valid expense with category | Expense saved by category and balance decreases | ok |
    | Add an income with a 0 or negative amount |  0 or negative amounts not allowed | ok - Input validation error |
    | Add a valid income with a large note | income saved succesfully | ok or bug :bug: - Notes does not have a limit of characters possible bug :question: the UI does not break and the note text is wrapped but it make sense to have large characters if they are wrapped and not possible to read ? |
    | Add a duplicated expense | expense saved succesfully | duplicated expenses are treated are new expenses and grouped by category|
    |play around with the category and dates views | balances and transaction shoudl remain intacted|

    | Findings |
    | - |
    | Pressing over the transaction enables an option to delete |
    | Pressing over a transaction enables an option to delete and you can select multiple transactions to be deleted |
    | Adding transactions by date is possible; however, the calendar allows selecting dates that are either too far in the past or future. Potential bug :bug: |
    | you can select account on whihc you want to add the transaction |
    | Red color assigned to Expenses and Green Color to Incomes|
    | Delete category is not allowed |
    | Selecting a transaction by category or date correctly enables deletion. However, changing the view retains the delete state, even though the selected transaction is no longer visibly reflected. This may indicate a potential bug :bug:|
    | Expanding all dates in the date-view filter is possible; however, switching views should collapse them. Instead, the dates remain expanded—even if you manually collapse them again UI-bug :bug: |
    | There is a balance foreach date and category|
    | You can edit transactions choosing different dates, categories and notes |
    | Categories or dates with no remaining transactions are removed automatically |

    

---

####  Charter 3: Transfer feature in Navigation ribbon
- **Goal:** Explore the Transfer feature through navigation ribbon and account selectors to discover transfer behavior across accounts
- **Set Up:** A minimum of two accounts already created, one account with initial balance
- **Priority:** Low - Transfers mainly benefit multi-account users and have workarounds through manual adjustments. Issues here have a lesser impact on the majority of users’ core budgeting tasks.
- **Scope:** Transfer Screen, Navigation Ribbon
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 10 minutes
- **Risks/Assumptions:** 
  - Transfer not displayed in the correct account
  - Transfer between the same account is not allowed
  - Transfer to multiple accounts is permitted
  - Deletion of a transfer is not permitted
- **Test Ideas:**
    | Test Idea | Expected Behavior |
    | - | - |
    | Transfer a valid amount from Account 1 to Account 2 | Deducted from account 1, added to account 2|
    | Transfer a negative amount | Error validation of incorrect amount |
    | Cancel transfer midway | No transaction created, balance unaffected |
    | Deletion of a existing transfer | Error validation Transfer cannot be deleted |

- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Valid transfer between different accounts | Deducted from account 1, added to account 2 succeeded | ok |
    | Transfer a 0 or negative amount  | Error validation of incorrect amount | ok |
    |Cancel transfer midway | No transaction created, balance unaffected| ok |
    |Deletion of a existing transfer | It was possible to delete an existing transferred transaction | The transferred transaction was deleted from account 2, and it went back to account 1. Is this the expected behavior, or is it a bug? :question: |
    |Try to transfer money within the same account |Validation error| ok |
    
    | Findings |
    | - |
    | I was able to transfer money from Account 1 to Account 2 even though Account 1 had no money. Is this a bug?  :question:|
    |Transfers are only allowed one at a time for a single account, not for multiple accounts at once|
    | I tried to transfer with non existing accounts, and it did not let me execute a transfer. instead it ask me to create an account|
    |The 'Add to Account' button is overlapped by the Android Home button. As a result, I was taken out of the app several times UI bug :bug:|
---

####  Charter 4: Account Switching in Navigation ribbon
- **Goal:** Explore the app’s global navigation ribbon, focusing on switching between accounts using the filter and search option to discover how account-specific data updates
- **Setup:** a minimum of two accounts and some expenses and incomes are already created
- **Priority:** High - Switching accounts is fundamental to accessing correct financial data. Errors here can cause major confusion, incorrect balances, and cdecrease reliability for users.
- **Scope:** Top ribbon bar, filter by date, search box, account switcher
- **Out of Scope**: Transfer feature, Options feature, Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 10 minutes
- **Risks/Assumptions:**
  - Search functionality does not reflect the right results 
  - Accounts switching breaks balance values
  - Filters does not reflect the right result  
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Switch to a different account (eg, “Card”) | Summary Ring and balance reflect the right account |
    | Search for existing note text (eg, “Loan”) | Results shown correctly |
    | Search for expense/income (eg, House/Salary) | Results shown correctly |
    | Filter by date | Summary Ring and balance reflect the right data based on filter|


- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Switch to a different account | Summary Ring and balance reflect the right account | ok |
    | Search for existing note text | The results correctly showed the transaction data | ok |
    | Search for expense/income | The results correctly showed the transaction data | ok |
    |Filter by date | The filter by day, week, month, Year, all word as expected | ok |
    |Filter by date | Issue with interval and choose date potential UI Bug |  - Old dates from deleted accounts are still visible Bug :bug: <br> - Filtering by a date with no transactions is allowed, and it also creates an extra Summary Ring screen with that particular date Bug :bug:  <br> - Filtering by interval date is not working Bug :bug:<br> - Choosing a specific date with no transactions is allowed, and it adds a period of blank days to the screen Bug :bug:|

---

####  Charter 5: Categories Management - Settings Panel
- **Goal:** Explore the Category Management screen using CRUD actions to discover how categories are managed and restricted
- **Scope:** Options slider, Category management screen
- **Priority:** Low - Category management is a customization feature that doesn’t directly affect the core budgeting calculations. Failures here impact personalization but don’t stop users from tracking expenses and incomes.
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 15 minutes
- **Risks/Assumptions:**
  - Some actions (eg, new categories) might be paid features
  - CRUD actions on categorys may corrupt icons or name
  - Grouping categories might not be allowed
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Try to add a new category | Feature locked behind paywall |
    | Try to edit an existing category with new emoji and name | Category updated with new emoji and name |
    | Duplicate a category | App should launch an error message |
    | Delete a category in use | Warning message pop ups|

- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Try to add a new category | This si a paid service | |
    | Try to edit an existing category with new emoji and name | Category updated with new name but emoji is not permitted is paid service | named updated succesfully. Updating the emoji is not allowed is this a bug? :question: |
    | Duplicate a category | duplication of category name is allowed | categories should have different name Bug :bug:|
    | Delete a category in use | warning message appeared and after confirmation the accounts was removed with its transactions| ok. <br> **note:** transaction dates of this removed account remain reflected in the summarry ring screen affecting the UI :bug: |
    | Edit an category name with empty name | validation error. Name required | ok |

    | Findings |
    | - |
    | Edits categories names does not affect the transactions balance |
    | Merging categories is allowed |
    | I can disable and enable categories|
    | I can delete categories, however I can't add them again. is this a bug:question:|


---

####  Charter 6: Accounts Management - Settings Panel
- **Goal:** Explore the Account Management screen using CRUD options to discover how account actions affect the balances
- **Scope:** Options slider, Account management screen
- **Priority:** Medium - Managing accounts directly affects balance accuracy and user control over finances. Mistakes in account management can misrepresent overall financial status, so this feature must maintain data integrity without compromising usability.
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 15 minutes
- **Risks/Assumptions:**
  - Deleting a existing account breaks the application
  - Creating new accounts is allowed
  - Merging accounts might not be possible
  - Accounts should have the same currency
  - Accounts does not allow billion transactions
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Add a new account named Savings and initial balance 1,000,000,000€ | New account should be created and initial balance should be reflected|
    | Edit an existing account | Account should be updated |
    | Duplicate a account | App should launch an error message |
    | Deleting an existing account | warning message should appeared and Account should be deleted |


- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Add a new account named Savings with different accounts balances including 1billion  and -999.999.999€ to 999.999.999€ | New account was created succesfully and initial balance was reflected | ok <br> - initial balance input allows several digits but saving controller only allows up (9 digits) |
    | Edit name and icon to an existing account | Account was updated succesfully | ok |
    |Duplicate a account| duplication of account was allowed| names should be unique :bug:|
    |Deleting an existing account| Account deletion is allowed| ok. Warning message launched, transactions are deleted|

    | Findings |
    | - |
    | Merging accounts is allowed |
    | Currency is a paid service|
    | I can disable and enable categories|
    | I can delete categories, however I can't add them again. is this a bug:question:|

---

####  Charter 7: Integration of Category Management screen and Summary Ring
- **Goal:** Explore Integration between Category and Summary Ring Screen using category modifications to discover their impact on summary ring widget
- **Setup:** a minimum of two categories are already created
- **Priority:** Medium - Integration issues can cause discrepancies between categories and summary views, confusing users about expense distribution.
- **Scope:** Category screen, summary ring screen
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 10 minutes
- **Risks/Assumptions:**
  - Categories might not reflect accurately in the summary ring
  - UI might crash with too many categories or emoji icons
  - Changing a category may not recalculate balances properly
  - Editing names/icons may cause summary display errors
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Add expense with new added category | New added category appears in summary ring reflecting the expenses added|
    | Change category of existing expense| Summary ring widget updates accordingly |
    | Delete used category | App warns and balance readjust accordingly |

- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Add expense with new added category | New added category appeared in summary ring reflecting the expenses added | ok |
    | Change category of existing expense | Summary ring widget updated accordingly | ok |
    | Delete used category | App warned and balance readjust accordingly | ok |

    | Findings |
    | - |
    |Deleted categories dissapeared from all UI screen. ok|
    |Deleted multiple categories and edit multiple categories crashed the application potential bug :bug: |
    |Important categories like 'Bills' remain visible in the system, even after deletion|
    |You can disable an categories preventing to add new transactions|
---

####  Charter 8:  Integration of Account Management Screen and Summary Ring Detail
- **Goal:** Explore Integration between Account and Summary Ring Screen using add/edit/delete actions to discover the impact on the summary and balances
- **Setup:** a minimum of two accounts are already created
- **Priority:** High - Accounts are the backbone of financial tracking. Any synchronization problems between account management and summaries risk inaccurate financial overviews
- **Scope:** Account management screen, account-related data in the summary ring and balance container
- **Out of Scope**: Stress Testing, Performance Profiling, Language Testing, Usability Testing
- **Duration:** 10 minutes
- **Risks/Assumptions:**
  - Deleting an account may leave orphaned transactions or break the summary ring
  - Balance totals may remain unchanged after account removal
  - Adding a new account might not be reflected immediately
- **Test Ideas:**

    | Test Idea | Expected Behavior |
    | - | - |
    | Delete an account with transactions | Related transactions removed, summary ring updates accordingly|
    | Add a new account with initial value | Summary Ring Screen showed new account with initial value |
    | Try to delete default or active account | App blocks or warns appropriately |

- **Observation and notes:**
    | Step Taken | What Happened | Issue/Comments/Question?  |
    | - | - |- |
    | Delete an account with transactions | Related transactions were moved and summary ring updated accordingly| ok |
    | Add a new account with initial value | Summary Ring  Screen showed new account with initial value | ok |
    | Try to delete default or active account| A warning message is shown with some considerations, the system allows the account to be deleted | ok|
    
    | Findings |
    | - |
    | You can merge accounts, but the total of their combined transactions is not calculated, resulting in the loss of previous transaction records. Potential bug :bug:|
    |You can disable an account preventing to add new transactions|

---

## Risk And Mitigation
| Risk                                                                                  | Mitigation                                                                                  |
|---------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| Data loss during app updates or crashes                                               | Implement local backup and cloud sync with automatic recovery logic                        |
| Risk of unreliable and non isolated test environment | Use containers or virtual environments to perform testing on pre-built environment |
| Risk of compatibility issues across different operating systems and screen sizes      | Run tests on cloud-based mobile device farms (e.g., BrowserStack, Sauce Labs)              |
| UI inconsistencies across platforms                                                   | Perform cross-platform and cross-resolution UI testing using visual regression tools. E.g AI tools can help on this    |
| potential Bugs caused by changes in other functionalities                                    | Maintain integration test coverage with CI validation performin regression test suite                                   |
| Risk of having limited tools for accessibility testing                                | Conduct manual accessibility testing using native OS tools (VoiceOver, TalkBack)           |
| Risk when testing inputs with different values                                         | Use boundary value analysis and equivalence partitioning to validate field behavior        |
| Risk of overlapping with native OS buttons or inaccessibility to native OS restrictions        | Manually verify views on various screen resolutions and OS versions and functionalities e.g wifi, bluetooth etc                        |
| Risk of improper handling of sensitive financial data                                 | Ensure production data is obfuscated or anonymized before being used in QA environments to prevent exposure of sensitive information         |
| Risk of incorrect balance calculations across multiple accounts                       | Validate financial logic through automated unit and component tests                      |