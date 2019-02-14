# SoAgile Planner #

SoAgile are a startup who build collaboration and management tools for agile teams. Their main product is SoAgile Planner which has functionality for sprint planning and collaboration via forums.

They've recently received a large round of funding and are now looking to to grow their customer base. Due to previous financial troubles SoAgile had to reduce their numbers and the original development team who built the app have now moved on.

With the new funding SoAgile have hired a new team of developers to grow the application. However after working on SoAgile Planner the new dev team have uncovered many design issues, especially around the language expressed in code. There is often a mismatch between how the business and maketing materials describe features and the naming of things in the codebase.

Furthermore SoAgile have promised investors that they can maximise their returns by breaking the sprint planning and collaboration functions of SoAgile Planner into two separate products and growing them individually. 

## Developer Interviews with the Business ##

To tackle the confusion of terminology used throughout the products the development team decided to setup some interviews with the business stakeholders. They used these interviews to agree on the language that everyone involved in the product would use going forward, both spoken and in the code itself. Two sessions were held to discuss the two newly proposed products.

---

### Sprint Planning Interview ###

For the sprint planning interview Julie, the lead developer is speaking to John about the sprint planning functionality:

**Julie:** So John, can you describe how our customers would use SoAgile Planner (SAP) to plan an iteration?

**John:** Sure! For one, we'd say how a team can use SAP to plan a sprint.

**Julie:** Ok great. How would a team do that?

**John:** Any team member can create a new sprint. Tickets can then be added to the sprint by any team member too. Team members can also add an estimate of how many story points the ticket will take to complete.

**Julie:** What's the life cycle of a ticket?

**John:** We say each ticket has a status and it moves through the states new, in progress and closed. Any team member can update a ticket's status by default but I believe we can configure the permissions for this.

**Julie:** Sure. How about removing tickets from the sprint?

**John:** Only the product owner can remove them.

**Julie:** How will the team know who's working on a ticket?

**John:** They can be assigned to a ticket by any team member, including themselves.

**Julie:** What happens if too many tickets are added to a sprint?

**John:** That shouldn't happen, if the total story points from all tickets exceeds the maximum planned for the sprint then we should show a message explaining this.

---

### Collaboration Interview ###

This time Julie is speaking to Jane about all things collaboration:

**Julie:** So Jane, can you describe how our customers would use SoAgile Planner (SAP) to collaborate?

**Jane:** Yeah sure. Our customers use SAP to open forums and start discussions about their work.

**Julie:** Forums and discussions you say? How about chats and threads?

**Jane:** Nope, I'm not sure what you mean, we don't have any chat functionality. What we do have is forums and discussions!

**Julie:** Ok, gotcha I think I know what you mean. So a forum is a collection of discussions?

**Jane:** Correct. And authors add posts in discussions. The author who starts a discussion is considered the owner of the discussion, other authors contributing are considered as participants.

**Julie:** How about posts, anything I should know about those?

**Jane:** There's not much to it, each post has a title, message and the date it was posted. We don't want massive titles so they're kept no longer than 250 characters, and messages no longer than 1000.

**Julie:** Thanks. So we've talked about opening forums, starting discussions and adding posts. Who can remove a discussion?

**Jane:** Only moderators can do that. They're a special type of author with the ability to remove discussions. You've got to have moderators, things can get a little heated!

**Julie:** Tell me about it, thanks Jane!

## To do ##

Feel free to tackle these in any order:

- Read the developer's interviews with the business folk, determine the ubiquitous language that is being used by the team for each product and reflect this in both the production code and tests.
- Is there anywhere an existing class represents more than one concept?
- Where could bounded context be applied and how could this be made explicit in the project?
- It looks like there is some sort of domain model but it's suffering from the [Anaemic Domain Model](https://www.martinfowler.com/bliki/AnemicDomainModel.html) anti-pattern. What behaviour can be moved into domain objects?

## Notes ##

This example is loosely based on the example Vaughn Vernon's [Implementing Domain-Driven Design](https://www.amazon.co.uk/Implementing-Domain-Driven-Design-Vaughn-Vernon/dp/0321834577).