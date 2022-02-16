# dotNet5782_9171_7973

## TODO
- [ ] Add links to PL Bonuses
- [ ] DataSource - FIX ALL PROBLEMS
- [ ] document
- [ ] finish README.md
- [ ] check deletion
- [ ] Remove `Send A parcel` in manager mode
- [ ] Center the progress bar customer
- [ ] Tool bar blue
- [ ] Hide text above progress bar

## Bugs
- [X] Notify parcel changed in simulatr
- [X] Let customer name to include space
- [ ] Rewrite messages
- [ ] Add deletion message in context menu
  
## 
- [ ] Unseen drones
- [ ] View parcel in simulator

## Bonus Review

### General
#### New c# Features
- [Record](
- [Switch Expression]
- [Tupples]
- [Init Only Setters]
- [Using Statement]
- [Range Operator]

### Dal

#### Structure
We have implemented the layers model in the second structure (The Bonus structur), So we have a config file which follos the given format:
```xml
<config>
    <dal>[chosen-dal]</dal>
    <dal-packages>
        <[package-1]>
            <class-name>[package-1-class-name]</class-name>
            <namespace>[package-1-namespace]</namespace>
        </[package-1]>
        ...
    </dal-packages>
</config>
```

This format allows to specify the namespace and not only the class name.

#### Logic Deletion
The dal deletion is just a *Logic Deletion* rather than *Real Deletion*. All the `Dal` entities implement the interface `IDeletable` which consists of just one property as follows
```csharp
interface IDeletable
{
    bool IsDeleted { get; set; }
}
```
From now, *Deletion* is just to change the `IsDeleted` property to `true`. Only non-deleted entities are allowed to perform actions.

#### Extensive Use of Generic
In order to prevent reapet our self according to the **DRY** rule, We implemnted all our `Dal` methods as generic methods. So, Instead of having `AddDrone`, `AddParcel`, `AddBaseStation` and `AddCustomer` for example, We only have `AddItem<T>` method.

### PL
#### Regular Expression
#### User Interface
Our project supports two modes: customer mode and manager mode. When you run the program you see

<img src="./screen-shots/sign-up.jpg" width="300">

You can press the `Sign In As Managar` and enter in manager mode or sign up for a new customer account and enter in customer mode. If you already have an account you can click on `Already Have an...` and get this scrren:

<img src="./screen-shots/sign-in.jpg" width="300">

you can always change mode by clicking `Log Out`

<img src="./screen-shots/log-out.jpg">

#### Full Support of All Data Queries
- Sort
- Filter by all properies
- Group by

#### MVVM
We used ***FULL*** MVVM.
##### Full Binding
##### PO Entities

#### Custom `UserControl`

#### Miscellaneous
- Triggres
  - Event Trigger [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/fc27f43be178a083ddce056caffc8e1395295442/dotNet5782_9171_7973/PL/Views/Style/StyleDictionary.xaml#L126)
- Behaviors example [definition](https://github.com/dasizis/dotNet5782_9171_7973/blob/for_exe_3/dotNet5782_9171_7973/PL/Views/Behaviors/DoubleInplutBehavior.cs#L6) [use](https://github.com/dasizis/dotNet5782_9171_7973/blob/5786b202b9f9332ab20bee664a9cc717ba732413/dotNet5782_9171_7973/PL/Views/AddCustomerView.xaml#L84)
- Converters [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/for_exe_3/dotNet5782_9171_7973/PL/Views/Converters/MessageBoxTypeToColorConverter.cs)
- Commands (We implemented A [`RelyCommand`](https://github.com/dasizis/dotNet5782_9171_7973/blob/for_exe_3/dotNet5782_9171_7973/PL/RelayCommand.cs) class and used it as properties in our `PL` classes) [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/5786b202b9f9332ab20bee664a9cc717ba732413/dotNet5782_9171_7973/PL/ViewModels/ParcelDetailsViewModel.cs#L23)
- Data templates
- `ObservableCollection` [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/5786b202b9f9332ab20bee664a9cc717ba732413/dotNet5782_9171_7973/PL/ViewModels/MainMapViewModel.cs#L15)
- `Collection View` [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/5786b202b9f9332ab20bee664a9cc717ba732413/dotNet5782_9171_7973/PL/ViewModels/FilteredListViewModel.cs#L53)

#### External dictionary



### Simulator

#### Location Update
<img src="./screen-shots/location-update.gif" height="200">

<img src="./screen-shots/map-update.gif">

#### Parallel Activation
You can activate as many drones as you want.

#### Busy Indicator
<img src="./screen-shots/busy-indicator.gif">

#### Maps
Each entity has it map to represent its location, Besides there is a `Main Map` for all the entities together.

### Design patterns
#### Factory - Full structure
#### Singleton
We implemnt an abstract class `Singleton` which has lazy initialization and is thread-safe. The `Dal` and `BL` layers just inherit it.


