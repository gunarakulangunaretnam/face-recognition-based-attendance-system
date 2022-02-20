import re
import cv2
import pyttsx3
import threading
import numpy as np
from pyzbar.pyzbar import decode
from scipy.spatial import distance as dist
from imutils import face_utils
import imutils
import time
import dlib
from playsound import playsound
import mysql.connector
import face_recognition
import pickle
import random
import ctypes
from datetime import datetime

mydb = mysql.connector.connect(

  host="localhost",
  user="root",
  passwd="",
  database="management_auto_attendance_system"
)


SelectDataCursor = mydb.cursor()


engine = pyttsx3.init()
en_voice_id = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens\TTS_MS_EN-US_ZIRA_11.0"  # female
ru_voice_id = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens\TTS_MS_RU-RU_IRINA_11.0"  # male
engine.setProperty('voice', en_voice_id)
rate = engine.getProperty('rate')
engine.setProperty('rate', rate - 20)


blink_Eye_Voice_Data = ["Please blink your eyes to comfirm your identity", "Can you please blink your eyes to comfirm your identity", "could you please blink your eyes to comfirm your identity", "You should blink your eyes to comfirm your identity", "ok dear, You must blink your eyes to comfirm your identity", "to comfirm your identity, please blink your eyes"]
try_again_Voice_Data = ["try again","please try again", "try once more", "try it again"]
unauthorized_face_Voice_Data = ["unauthorized face found. access denied", "your face does not match with the database signature, access denied", "your face is not found in the database, access denied", "access denied. access denied, unauthorized face found"]
unauthorized_IDcard_Voice_Data = ["unauthorized identity card found. access denied. access denied.", "access denied. access denied. unauthorized identity card found.", "your identity card is invalid. access denied", "invalid identity card access denied", "i can't accept your identity card. because it is invalid","Your identity card is incorrect, access denied"]

Entering_Voice_Data = [" ","you look smart today","it's nice to see you","Wow, you are awesome today", "you are so beautiful today", "you are wearing a nice dress", "I love you, because, you are so attractive","you look smart today, and please focus on your work","please work hard","please focus on your work","I love you very much, because you are a hard worker"]
Exiting_Voice_Data = ["you look so tired"," ","you did a good job today","you made it today","today, you performed well","i think you feel sad","i think you feel tired","i think you feel good","how about your work today?","how was today","i think, today you were happy at your work","i think, you enjoyed your work today","i think, you felt sad on your work","you did a nice job today","i think, you are angry on your work today"]

Entering_Attendance_Passed = ["ok done, your attendance has been marked, You are welcome and have a nice day", "your attendance has been marked, You are welcome and have a nice day","your attendance has been recorded successfully. have a nice day", "you are welcome, your attendance has been marked","you are warmly welcome and your attendance has been marked successfully","your attendance has been marked successfully. have a wonderful day", "your attendance has been marked. Please focus on your work","your attendance has been noted, i hope, you would perform well on your work today","thank you, your attendance has been marked","you are welcome, your attendance has been recorded","your attendance has been noted and you are warmly welcome","your attendance has been marked successfully, and please concentrate on your work"]
Exiting_Attendance_Passed = ["Ok done, i will see you next time","ok thanks for working hard today, i will see you next time","ok done. thanks for working, go and spend time with your family","ok done. we will meet you next time.","ok done. take care sweety","ok done. go and take a rest, because you seems like tired","ok done dear, we will meet next time.","ok you are done, bye and take care of yourself","ok done. go and take a rest dear","ok you are done. You did a wonderful job today. thanks for that","ok done, i will see you next time","ok you are done, we will meet next day dear","ok done, take care of yourself","ok my dear, you are done, go and take a good rest"]

Restarting_Voice = ["The system is restarting", "I am restarting myself", "I am rebooting","the system is rebooting", "i am deactivating myself","the face recognition process has been canceled"]

Entering_Attendance_Passed_Pending = ["Your attendance marked as pending. Your request will be reviewed by the admin. Anyway, You are welcome and have a nice day", "Your attendance in pending. the admin will review it soon, you're welcome and have a wonderful day", "your attendance noted as pending, admin will review it soon. you are warmly welcome and have a nice day", "your attendance request has been sent to the admin for review. you are welcome and have a great day", "your attendance request has been recorded. it will be review by the admin soon. and have a nice day"]

shutting_Down_Voices = ["The system is shutting down", "Bye, I am shutting down myself","The system is deactivating", "Bye, I am deactivating myself", "The system is turning off", "Bye, I am turning off myself"]


Attendance_Already_Exit = ["your attendance has been already marked","your attendance has been already exited in the database", "your attendance is already in the database"]


user32 = ctypes.windll.user32
screensize = user32.GetSystemMetrics(0), user32.GetSystemMetrics(1)

Screen_Width = 820
Screen_Height = 620

Screen_Center_Width = int((screensize[0] - Screen_Width) / 2)
Screen_Center_Height = int((screensize[1] - Screen_Height) / 2) - 30


now = datetime.now()
Global_Current_Date = now.strftime("%d-%m-%Y")


Counter = 0

Employee_ID = ""
Employee_Name = ""
Attendance_Type = ""

EYE_AR_THRESH = 0.3
EYE_AR_CONSEC_FRAMES = 2
# initialize the frame counters and the total number of blinks
Eye_COUNTER = 0
TOTAL_Blinks = 0

# initialize dlib's face detector (HOG-based) and then create
# the facial landmark predictor
print("[INFO] loading facial landmark predictor...")
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor("Shape_Model\\shape_predictor_68_face_landmarks.dat")

# grab the indexes of the facial landmarks for the left and
# right eye, respectively
(lStart, lEnd) = face_utils.FACIAL_LANDMARKS_IDXS["left_eye"]
(rStart, rEnd) = face_utils.FACIAL_LANDMARKS_IDXS["right_eye"]


face_exception = "";

f = open("System_Settings\\Face_rec.txt")
face_exception = f.read()
f.close()

def talk_function(audio):
    print("Computer: {}".format(audio))
    engine.say(audio)
    engine.runAndWait()

def greeting_function():
    currentHour = int(datetime.now().hour)
    basicGreeting = ""

    if currentHour >= 0 and currentHour < 12:
        basicGreeting = "Good Morning"

    if currentHour >= 12 and currentHour < 18:
        basicGreeting = "Good Afternoon"

    if currentHour >= 18 and currentHour != 0:
        basicGreeting = "Good Evening"

    return basicGreeting


def eye_aspect_ratio(eye):
    # compute the euclidean distances between the two sets of
    # vertical eye landmarks (x, y)-coordinates
    A = dist.euclidean(eye[1], eye[5])
    B = dist.euclidean(eye[2], eye[4])
    # compute the euclidean distance between the horizontal
    # eye landmark (x, y)-coordinates
    C = dist.euclidean(eye[0], eye[3])
    # compute the eye aspect ratio
    ear = (A + B) / (2.0 * C)
    # return the eye aspect ratio
    return ear


cap = cv2.VideoCapture(0)

def Insert_to_database(Qurry, Data):

    insertCursor = mydb.cursor()
    insertCursor.execute(Qurry, Data)
    mydb.commit()


def MainSystem():
    global Counter, cap, TOTAL_Blinks, Eye_COUNTER, Employee_Name, Employee_ID, Attendance_Type, Global_Current_Date,face_exception 

    ###############
    #    Reset    #
    ###############

    TOTAL_Blinks = 0
    Eye_COUNTER = 0
    Employee_ID = ""
    Employee_Name = ""
    Attendance_Type = ""

    ###############
    #    Reset    #
    ###############

    while True:
 
        success, img = cap.read()
        img = imS = cv2.resize(img, (Screen_Width, Screen_Height))
        #img = imS = cv2.resize(img, (screensize[0], screensize[1]))
        #cv2.namedWindow("Display_1", cv2.WND_PROP_FULLSCREEN)
        #cv2.setWindowProperty("Display_1",cv2.WND_PROP_FULLSCREEN,cv2.WINDOW_FULLSCREEN)
     
        QRcodeData = decode(img)

        if len(QRcodeData) !=0:
            CodeType = QRcodeData[0][1]

            if CodeType == "QRCODE":
                
                hiddenData = QRcodeData[0][0].decode('utf-8')

                if Counter == 5:

                	Counter = 0

                	SelectDataCursor.execute("SELECT first_name, last_name FROM employees WHERE employee_id = '{}' AND is_model_available = 'True'".format(hiddenData))
                	collecttedData = SelectDataCursor.fetchall()

                	if not collecttedData:
                		talk_function(random.choice(unauthorized_IDcard_Voice_Data))
                	
                	else:

                		SelectDataCursor.execute("SELECT * FROM attendance WHERE _date = '{}' and employee_id = '{}' and out_time != ''".format(Global_Current_Date, hiddenData))
                		AlreadyExit = SelectDataCursor.fetchall()

                		if not AlreadyExit:             		
                		
	                		SelectDataCursor.execute("SELECT out_time FROM attendance WHERE employee_id = '{}'".format(hiddenData))
	                		attendance_type_data = SelectDataCursor.fetchall()

	                		if not attendance_type_data:
	                			Attendance_Type = "Entering"

	                		else:
	                			Attendance_Type = "Exiting"

	                		
	                		Employee_ID = hiddenData
	                		Employee_Name = collecttedData[0][0]+" "+collecttedData[0][1]
	                		greeting = greeting_function()
	                		
	                		playsound("AttendanceSystemData\\QRSound.mp3")
	                		
	                		if Attendance_Type == "Entering":
	                			talk_function("{} {}. {}".format(greeting,Employee_Name,random.choice(Entering_Voice_Data)))
	                			

	                		elif Attendance_Type == "Exiting":
	                			talk_function("{} {}. {}".format(greeting,Employee_Name,random.choice(Exiting_Voice_Data)))
	                			

	                		cv2.destroyAllWindows()
	                		break
	                	
	                	else:

	                		talk_function(random.choice(Attendance_Already_Exit))


                pts = np.array([QRcodeData[0][3]],np.int32)
                pts = pts.reshape((-1,1,2))
                cv2.polylines(img,[pts],True,(0,255,0),5)

                Counter +=1

        cv2.imshow('Display_1',img)
        cv2.moveWindow('Display_1', Screen_Center_Width, Screen_Center_Height) ##center window
        key = cv2.waitKey(1)


        if key == 27:
        	talk_function(random.choice(shutting_Down_Voices))
        	exit()


    talk_function(random.choice(blink_Eye_Voice_Data))

    while True:

        success, frame = cap.read()
        frame = imS = cv2.resize(frame, (Screen_Width, Screen_Height))
        #frame = imS = cv2.resize(frame, (screensize[0], screensize[1]))
        #cv2.namedWindow("Display_2", cv2.WND_PROP_FULLSCREEN)
        #cv2.setWindowProperty("Display_2",cv2.WND_PROP_FULLSCREEN,cv2.WINDOW_FULLSCREEN) 

        CacheFrame = frame.copy()

        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        # detect faces in the grayscale frame
        rects = detector(gray, 0)
            # loop over the face detections
        for rect in rects:
            # determine the facial landmarks for the face region, then
            # convert the facial landmark (x, y)-coordinates to a NumPy
            # array
            shape = predictor(gray, rect)
            shape = face_utils.shape_to_np(shape)
            # extract the left and right eye coordinates, then use the
            # coordinates to compute the eye aspect ratio for both eyes
            leftEye = shape[lStart:lEnd]
            rightEye = shape[rStart:rEnd]
            leftEAR = eye_aspect_ratio(leftEye)
            rightEAR = eye_aspect_ratio(rightEye)
            # average the eye aspect ratio together for both eyes
            ear = (leftEAR + rightEAR) / 2.0

            # compute the convex hull for the left and right eye, then
            # visualize each of the eyes
            leftEyeHull = cv2.convexHull(leftEye)
            rightEyeHull = cv2.convexHull(rightEye)
            cv2.drawContours(frame, [leftEyeHull], -1, (0, 255, 0), 1)
            cv2.drawContours(frame, [rightEyeHull], -1, (0, 255, 0), 1)

            # check to see if the eye aspect ratio is below the blink
            # threshold, and if so, increment the blink frame counter
            if ear < EYE_AR_THRESH:
                Eye_COUNTER += 1
            # otherwise, the eye aspect ratio is not below the blink
            # threshold
            else:
                # if the eyes were closed for a sufficient number of
                # then increment the total number of blinks
                if Eye_COUNTER >= EYE_AR_CONSEC_FRAMES:
                    TOTAL_Blinks += 1
                    
                # reset the eye frame counter
                Eye_COUNTER = 0

        if TOTAL_Blinks == 2:
        	TOTAL_Blinks = 0
        	cv2.imwrite("Cache/CacheImg.jpg",CacheFrame)
        	playsound("AttendanceSystemData\\blinkSound.mp3")
        	data = pickle.loads(open("Trained_Models\\{}\\{}_(Model).pickle".format(Employee_ID,Employee_ID), "rb").read())
        	image = cv2.imread("Cache\\CacheImg.jpg")
        	rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        	boxes = face_recognition.face_locations(rgb, model='hog')

        	if(len(boxes) == 1): #if it finds only one face

        		BreakStatus = False
        		
        		encodings = face_recognition.face_encodings(rgb, boxes)

        		for encoding in encodings:
        			matches = face_recognition.compare_faces(data["encodings"], encoding, 0.5)

        			if True in matches:

        				now = datetime.now()
        				currentTime = now.strftime("%I:%M:%S %p")

        				BreakStatus = True

        				if Attendance_Type == "Entering":
        					
        					#Insert Query
        					insert_cursor = mydb.cursor()
        					sqlCode = "INSERT INTO attendance VALUES ('{}', '{}', '{}','{}', '{}', '{}','{}','{}','{}')".format("",Employee_ID, currentTime, "",Global_Current_Date, "True","","","")
        					insert_cursor.execute(sqlCode)
        					mydb.commit()

        					talk_function(random.choice(Entering_Attendance_Passed))

        				elif Attendance_Type == "Exiting":
        					
        					#Update Query
        					update_cursor = mydb.cursor()
        					MySqlCode = "UPDATE attendance SET out_time = '{}', face_recognition_exiting = 'True' WHERE employee_id = '{}'".format(currentTime,Employee_ID)
        					update_cursor.execute(MySqlCode)
        					mydb.commit()

        					talk_function(random.choice(Exiting_Attendance_Passed))
        					
        			else:
        				talk_function(random.choice(unauthorized_face_Voice_Data))
        				BreakStatus = False
        			
        		if BreakStatus == True:
        			break

        	else:
        		talk_function(random.choice(try_again_Voice_Data))


        	
        # show the frame
        cv2.imshow("Display_2", frame)
        cv2.moveWindow('Display_2',Screen_Center_Width, Screen_Center_Height) #center window
        key = cv2.waitKey(1)

        if key == 32:
        	talk_function(random.choice(Restarting_Voice))

        	break

        elif key == 13:
        	
        	if face_exception.strip() == "True":
   
	        	now = datetime.now()
	        	currentTime = now.strftime("%I:%M:%S %p")
	        	timestampStr = now.strftime("%d-%b-%Y_(%H-%M-%S-%f)")
	        	

	        	if Attendance_Type == "Entering":

	        		img_name = "{}_Entering.jpg".format(timestampStr)
	        		#Insert Query
	        		cv2.imwrite("Attendance_Pending_Images/{}".format(img_name),frame)
	        		insert_cursor = mydb.cursor()
	        		sqlCode = "INSERT INTO attendance VALUES ('{}', '{}', '{}','{}', '{}', '{}','{}','{}','{}')".format("",Employee_ID, currentTime,"",Global_Current_Date,"False","",img_name,"")
	        		insert_cursor.execute(sqlCode)
	        		mydb.commit()

	        		talk_function(random.choice(Entering_Attendance_Passed_Pending))
	        	
	        	elif Attendance_Type =="Exiting":

	        		img_name = "{}_Exiting.jpg".format(timestampStr)
	        		#Update Query
	        		cv2.imwrite("Attendance_Pending_Images/{}".format(img_name),frame)
	        		update_cursor = mydb.cursor()
	        		MySqlCode = "UPDATE attendance SET out_time = '{}', face_recognition_exiting = 'False', face_recognition_exiting_img_path  = '{}' WHERE employee_id = '{}'".format(currentTime,img_name, Employee_ID)
	        		update_cursor.execute(MySqlCode)
	        		mydb.commit()

	        		talk_function(random.choice(Exiting_Attendance_Passed))
	     	
	        	break
	        
	        elif key == 27:
	        	talk_function(random.choice(shutting_Down_Voices))
	        	exit()



    cv2.destroyAllWindows()
    MainSystem()
     


if __name__ == "__main__":
    MainSystem()
