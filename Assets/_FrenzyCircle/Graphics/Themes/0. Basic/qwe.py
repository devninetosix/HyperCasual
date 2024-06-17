from PIL import Image, ImageOps, ImageDraw

# Load the image
image_path = "0. rotCircle.png"
image = Image.open(image_path)

# Create a mask to remove the border
mask = Image.new('L', image.size, 0)
draw = ImageDraw.Draw(mask)
draw.pieslice([(0, 0), image.size], 0, 360, fill=255)

# Apply the mask to the image
image.putalpha(mask)

# Save the output
output_path = "qweqwe.png"
image.save(output_path)
output_path
