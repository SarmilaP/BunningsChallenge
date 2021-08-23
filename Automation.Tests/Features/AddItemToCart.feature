Feature: Verify Items in Cart
	As a guest user
	I want to be select,add items to the cart and review the same
	So I can place the order with correct details


@UI
Scenario: Search for an item and verify the search results
	Given I have navigated to the Bunnings site
	When I search for Hammer
	Then I verify the results of the search are displayed

@UI
Scenario: Add and review items in the cart
	Given I have navigated to the Bunnings site
	And I search for Hammer
	When I add the first item listed to the cart
	Then I verify the result in the cart macth the 1

@UI
Scenario Outline: Add and update items in the cart
	Given I have navigated to the Bunnings site
	And I search for Hammer and add it to the cart
	When I update the <item count> in the cart
	Then I verify the result in the cart macth the <item count>

Examples:
| item count |
| 3          |
| 0          |


@UI
Scenario: Add multiple items to the cart
	Given I have navigated to the Bunnings site
	When I search for item and add the first item to cart
	|SearchItem|
	|Nail|
	|Tile|
	Then I verify the items in the cart match the 2 items added
	

