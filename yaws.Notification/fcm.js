const admin = require('firebase-admin');
const dotenv = require('dotenv');

dotenv.config();

// init firebase admin
const serviceAccount = require(process.env.GOOGLE_SERVICE_ACCOUNT);
admin.initializeApp({
  credential: admin.credential.cert(serviceAccount),
  databaseURL: process.env.FIREBASE_DATABASE_URL
});


function send(options) {
  let condition = `'${options.platform}_${options.stat}' in topics`;

  let message = {
    notification: {
      title: options.title,
      body: options.body,
    },
    android: {
      notification: {
        title: options.title,
        body: options.body,
        visibility: 'PUBLIC' // show notification on lock screen
      }
    },
    condition: condition
  };

  console.log('Sending FCM: ');
  console.log(message);

  return new Promise((resolve, reject) => {
    admin.messaging().send(message)
    .then((response) => {
      // Response is a message ID string.
      console.log('Successfully sent message:', response);
      resolve(response);
    })
    .catch((error) => {
      console.log('Error sending message:', error);
      reject(error);
    });
  });
}

module.exports = {
  send
}