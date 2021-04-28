module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    const firstName = req.query.firstname;
    const lastName = req.query.lastname;

    if (!firstName || !lastName) {
        context.res = {
            body: "Please provide your first and last name parameters."
        }
        return;
    }

    context.res = {
        body: 'first name: Bartosz, last name: Wackowski'
    }
}