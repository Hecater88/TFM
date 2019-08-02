using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ImageSlicer{
	/// <summary>
	/// Método para cortar la imagen en bloques
	/// </summary>
	/// <returns>Imagen.</returns>
	/// <param name="image">Image.</param>
	/// <param name="blocksPerLine">Blocks per line.</param>
	public static Texture2D[,] GetSlices(Texture2D image, int blocksPerLine){

		//guardamos el tamaño de la imagen en una variable, eligimos el tamaño minimo entre el ancho y el alto
		int imageSize = Mathf.Min (image.width, image.height);
		//dividimos el valor del tamaño de la imagen por el numero de bloques que queremos en una linea, para obtener el tamaño de los bloques
		int blockSize = imageSize / blocksPerLine;
		//creamos una textura 2D con un tamaño en base al numero de bloques que queremos por linea
		Texture2D[,] blocks = new Texture2D[blocksPerLine, blocksPerLine];

		//Recorremos toda la textura 2D
		for (int i = 0; i < blocksPerLine; i++) {
			for (int j = 0; j < blocksPerLine; j++) {
				//creamos una variable temporal para lo bloques
				Texture2D block = new Texture2D (blockSize, blockSize);
				//y en cada bloque le colocamos el trozo de imagen que le corresponde
				block.wrapMode = TextureWrapMode.Clamp;
				block.SetPixels (image.GetPixels (j * blockSize, i * blockSize, blockSize, blockSize));
				block.Apply ();
				//igualamos la variable temporal block a blocks
				blocks [j, i] = block;
			}
		}
		return blocks;

	}
}
