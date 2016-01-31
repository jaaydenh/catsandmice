[System.Serializable]
public struct IntVectorTwo {

	public int x, y;

	public IntVectorTwo (int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static IntVectorTwo operator + (IntVectorTwo a, IntVectorTwo b) {
		a.x += b.x;
		a.y += b.y;
		return a;
	}
}