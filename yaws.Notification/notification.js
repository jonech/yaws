const cron = require('node-cron');
const express = require('express');
//const fs = require('fs');

const admin = require('firebase-admin');
const dotenv = require('dotenv');
const api = require('./src/api');
const wfstat = require('./src/wfstat');
const constant = require('./src/constant');
const fcm = require('./fcm');
const scheduler = require('./src/scheduler');

dotenv.config();

// init firebase admin
// const serviceAccount = require(process.env.GOOGLE_SERVICE_ACCOUNT);
// admin.initializeApp({
//   credential: admin.credential.cert(serviceAccount),
//   databaseURL: process.env.FIREBASE_DATABASE_URL
// });

var port = process.env.PORT;

const app = express();

// app.get('/ping', async(req, res) => {
//   let worldState = await api.getWorldState();
//   if (worldState.cetusCycle && worldState.cetusCycle.expiry) {
//     let newCetusCycle = worldState.cetusCycle;
//     let expiry = newCetusCycle.expiry;
//     let timeLeft = expiry - Date.now();
//     if (timeLeft <= 0) {
//       res.send({oldstuff: newCetusCycle});
//       return;
//     }

//     let oldCetusCycle = await wfstat.getStatByStatType(constant.WFStatType.CETUS_CYCLE);
//     if (oldCetusCycle == null) {
//       res.send({oldstuff: oldCetusCycle});
//       return;
//     }
//     if (oldCetusCycle.statId == newCetusCycle.id) {
//       res.send({oldstuff: oldCetusCycle});
//       return;
//     }

//     await wfstat.deleteRow(oldCetusCycle.dbId);
//     await wfstat.insertRow(newCetusCycle.id, constant.WFStatType.CETUS_CYCLE);

//     fcm.send(`Cetus ${newCetusCycle.state}`, newCetusCycle.shortString, constant.WFPlatform.PC, constant.WFStatType.CETUS_CYCLE);
//     res.send({newstuff: newCetusCycle});
//   }

// });

// cron.schedule('* * * * *', async () => {
//   console.log('---------------------');
//   console.log('Running Cron Job');

//   console.log('Fetching WorldState...');
//   let worldState = await api.getWorldState();
//   console.log('Fetch WorldState complete');
//   if (worldState) {
//     await processCetusCycle(worldState.cetusCycle);
//   }
//   console.log('Cron Job complete...');
// });

app.get('/ping', async (req, res) => {
  res.send({result: 'OK'});
})

scheduler.runCetusCycle();

app.listen(port, () => console.log(`listening on http://localhost:${port}`));


async function processCetusCycle(newCetusCycle) {
  if (newCetusCycle && newCetusCycle.expiry) {
    let expiry = newCetusCycle.expiry;
    let timeLeft = expiry - Date.now();
    if (timeLeft <= 0) {
      return;
    }

    let oldCetusCycle = await wfstat.getStatByStatType(constant.WFStatType.CETUS_CYCLE);
    if (oldCetusCycle == null) {
      return;
    }
    if (oldCetusCycle.statId == newCetusCycle.id) {
      return;
    }

    await wfstat.deleteRow(oldCetusCycle.dbId);
    await wfstat.insertRow(newCetusCycle.id, constant.WFStatType.CETUS_CYCLE);

    fcm.send(`Cetus ${newCetusCycle.state}`, newCetusCycle.shortString, constant.WFPlatform.PC, constant.WFStatType.CETUS_CYCLE);
  }
}

