# dotNet5782_9171_7973

## TODO
- [ ] Add links to PL Bonuses
- [X] DataSource - FIX ALL PROBLEMS
- [X] document
- [ ] finish README.md
- [ ] check deletion
- [X] Remove `Send A parcel` in manager mode
- [X] Center the progress bar customer
- [ ] Tool bar blue
- [ ] Hide text above progress bar

## Bugs
- [X] Notify parcel changed in simulatr
- [X] Let customer name to include space
- [X] Rewrite messages
- [X] Add deletion message in context menu
  
## 
- [ ] Unseen drones
- [ ] View parcel in simulator

## Bonus Review

### General
#### New c# Features
- [Record](https://github.com/dasizis/dotNet5782_9171_7973/blob/b3326b502c4ef8df432f34f2dca4ba10b42f6404/dotNet5782_9171_7973/BL/BO/DroneSimulatorChanges.cs#L1)
- [Switch Expression](https://github.com/dasizis/dotNet5782_9171_7973/blob/b3326b502c4ef8df432f34f2dca4ba10b42f6404/dotNet5782_9171_7973/BL/BL/BL.cs#L211)
- [Tupples](https://github.com/dasizis/dotNet5782_9171_7973/blob/b3326b502c4ef8df432f34f2dca4ba10b42f6404/dotNet5782_9171_7973/BL/BL/BL.cs#L33)
- [Init Only Setters](https://github.com/dasizis/dotNet5782_9171_7973/blob/b3326b502c4ef8df432f34f2dca4ba10b42f6404/dotNet5782_9171_7973/BL/BL/BL.cs#L21)
- [Using Statement](https://github.com/dasizis/dotNet5782_9171_7973/blob/52a512db91d8e25589d7bedd44bb86eb4fbb7ad7/dotNet5782_9171_7973/DalXml/XSerialization.cs#L18)
- [Range Operator](https://github.com/dasizis/dotNet5782_9171_7973/blob/52a512db91d8e25589d7bedd44bb86eb4fbb7ad7/dotNet5782_9171_7973/StringUtilities/StringUtilities.cs#L42)

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
From now, *deletion* is changing the entity's `IsDeleted` property to `true`. Only non-deleted entities are allowed to perform actions.

#### Extensive Use of Generic
In order to Avoid repetition according to the **DRY** rule, We implemented all our `Dal` methods as generic methods. So, Instead of having `AddDrone`, `AddParcel`, `AddBaseStation` and `AddCustomer` for example, We only have `AddItem<T>` method.

### PL
#### Custom Window Layout - Docking
Layout is very flexible and easy to use. 
- [ ] gif
#### Regular Expression
#### User Interface
Our project supports two modes: customer mode and manager mode. When you run the program you see

<img src="./screen-shots/sign-up.jpg" width="300">

You can press the `Sign In As Managar` and enter in manager mode or sign up for a new customer account and enter in customer mode. If you already have an account you can click on `Already Have an...` and get this screen:

<img src="./screen-shots/sign-in.jpg" width="300">

you can always change mode by clicking `Log Out` and reconnecting.

<img src="./screen-shots/log-out.jpg">

#### Full Support of All Data Queries
Very easy way to accsses accurate data.
- Sort 
-  [ ] gif
- Filter by all properies
-  [ ] gif
- Group by 
- [ ] gif

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
- Commands (We implemented A [`RelayCommand`](https://github.com/dasizis/dotNet5782_9171_7973/blob/for_exe_3/dotNet5782_9171_7973/PL/RelayCommand.cs) class and used it as properties in our `PL` classes) [example](https://github.com/dasizis/dotNet5782_9171_7973/blob/5786b202b9f9332ab20bee664a9cc717ba732413/dotNet5782_9171_7973/PL/ViewModels/ParcelDetailsViewModel.cs#L23)
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
- [ ] gif

#### Busy Indicator
<img src="./screen-shots/busy-indicator.gif">

#### The Application Prevent Closing As long as Simulators are On
This is to make sure all data is fully updated.
- [ ] gif

#### Maps
Each entity has its map to represent its location, Besides there is a `Main Map` for all the entities together.

### Design patterns
#### Factory - Full structure
#### Singleton
We implemnt an abstract class `Singleton` which has lazy initialization and is thread-safe. The `Dal` and `BL` layers just inherit it.


###Last But Not Least - Well Neat, Organized and Detailed `README`.
