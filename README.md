Unity Developer Selection Project

3 Parts:
- 1st: Read data from a JSON as if it were an API.
- 2nd: Some screens like a form/wizard/onboarding.
- 3rd: Display the results based on what the user selected in the previous screens.

1 - Collection Screen:
- Title at the top/center.
- Next button in the upper right corner.
- Display a grid of cards in the center of the screen based on the array from the JSON collections.
- The user can click on a card to select one of the collections; when doing this, the screen keeps track of the selected card's ID.
- The Next button is disabled until the user selects a card.

2 - Item Screen:
- Title at the top/center.
- Next button in the upper right corner.
- Back button in the upper left corner; the user can go back to the collection screen, where they can see what was previously selected, change their selection, and continue.
- Display a grid of cards in the center of the screen based on the array from the JSON area/autograph/imprint. If the user chose "area" on the collection screen, only display the content from the JSON "area," and so on.
- The user can click on a card to select one of the items; when doing this, the screen keeps track of the selected card's ID.
- The Next button is disabled until the user selects a card.

3 - Size Screen:
- Title at the top/center.
- Next button in the upper right corner.
- Back button in the upper left corner; the user can go back to the item screen, where they can see what was previously selected, change their selection, and continue.
- Number input: "Width" in centimeters.
- Number input: "Depth" in centimeters.

4 - Final Screen:
- Display a box in the center with the dimensions provided on the size screen. If the user entered 20x20, then display a box of 20 by 20; if the user entered 20x10, display a box of 20x10.
- In the upper left corner, display which collection was selected. If the user selected the ID "area," display the text "Area Collection," and so on.
- In the upper right corner, display which item was selected. If the user selected the ID "area-01," display the text "Area Item 01," and so on.

Bonus, important to leave for last:
- Display an image on the card instead of just a button with text (for now, it can be any image; what matters is the example).
- Do not use scaling to size the cube; instead, generate a mesh.
- Make it an app.

Important Considerations:
- When performing the test, I will test with different JSON files using different values, where the text may have more/less characters to display on the card, or where there may be more or less than 3 elements—perhaps 5 collections, maybe 1, perhaps 10 items. It is important that the app can read the JSON and display the data contained within, so that if we switch to a JSON with more/less data (obviously keeping the same fields), it continues to work.
- The interface does not need to be beautiful or perfect, but it should not break anything.
- Reusable code is better.
- Create the project using URP (Universal Render Pipeline).

Maximum deadline: 10 hours from receiving this explanation.