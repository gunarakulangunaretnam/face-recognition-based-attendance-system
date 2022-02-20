# import the necessary packages
from imutils import paths
import face_recognition
import argparse
import pickle
import qrcode
import cv2
import os

ap = argparse.ArgumentParser()
ap.add_argument("-e", "--employee_id", required=True,
	help="Enter the Employee ID")
args = vars(ap.parse_args())

#Creating Model Folder
directory = "Trained_Models\\{}".format(args['employee_id'])
if not os.path.exists(directory):
    os.makedirs(directory)


def Genarate_QR_Code(value):
	qr = qrcode.QRCode(version=3, error_correction=qrcode.constants.ERROR_CORRECT_M,box_size=8, border=6)
	qr.add_data(value)
	qr.make(fit=True)
	img = qr.make_image()
	img.save("{}\\{}_(QRCode).jpg".format(directory,args['employee_id']))


Genarate_QR_Code(args['employee_id'])


# grab the paths to the input images in our dataset
print("[INFO] quantifying faces...")
imagePaths = list(paths.list_images("Datasets\\{}\\".format(args['employee_id'])))
# initialize the list of known encodings and known names
knownEncodings = []
knownNames = []

# loop over the image paths
for (i, imagePath) in enumerate(imagePaths):
	# extract the person name from the image path
	print("[INFO] processing image {}/{}".format(i + 1,
		len(imagePaths)))

	try:
		f = open("Training_Status.txt","w")
		f.write("{}|{}".format(i+1,len(imagePaths)))
		f.close()
	except Exception as e:
		pass

	name = imagePath.split(os.path.sep)[-2]
	# load the input image and convert it from BGR (OpenCV ordering)
	# to dlib ordering (RGB)
	image = cv2.imread(imagePath)
	rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

	# detect the (x, y)-coordinates of the bounding boxes
	# corresponding to each face in the input image
	boxes = face_recognition.face_locations(rgb,
		model="hog")
	# compute the facial embedding for the face
	
	encodings = face_recognition.face_encodings(rgb, boxes)
	
	# loop over the encodings
	for encoding in encodings:
		# add each encoding + name to our set of known names and
		# encodings
		knownEncodings.append(encoding)
		knownNames.append(name)

# dump the facial encodings + names to disk
print("[INFO] serializing encodings...")
data = {"encodings": knownEncodings, "names": knownNames}
f = open("Trained_Models\\{}\\{}_(Model).pickle".format(args['employee_id'],args['employee_id']), "wb")
f.write(pickle.dumps(data))
f.close()

